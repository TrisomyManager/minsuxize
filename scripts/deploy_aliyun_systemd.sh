#!/usr/bin/env bash
set -euo pipefail

APP_ROOT="${APP_ROOT:-${APP_DIR:-/var/www/minsuxize}}"
REPO_DIR="${REPO_DIR:-$APP_ROOT/repo}"
REPO_URL="${REPO_URL:-https://github.com/TrisomyManager/minsuxize.git}"
BRANCH="${BRANCH:-main}"
SERVICE_NAME="${SERVICE_NAME:-minsuxize.service}"
PROJECT_PATH="${PROJECT_PATH:-src/MinsuXize.Web/MinsuXize.Web.csproj}"
PUBLISH_DIR="${PUBLISH_DIR:-$APP_ROOT/publish}"
DATA_DIR="${DATA_DIR:-$APP_ROOT/App_Data}"
BACKUP_DIR="${BACKUP_DIR:-$APP_ROOT/backups}"
HEALTH_URL="${HEALTH_URL:-http://127.0.0.1:5000/healthz}"
EXPECTED_DLL="${EXPECTED_DLL:-$PUBLISH_DIR/MinsuXize.Web.dll}"
SKIP_SERVICE_PATH_CHECK="${SKIP_SERVICE_PATH_CHECK:-false}"

log() {
  printf '[deploy] %s\n' "$1"
}

require_command() {
  if ! command -v "$1" >/dev/null 2>&1; then
    printf 'Required command not found: %s\n' "$1" >&2
    exit 1
  fi
}

require_command git
require_command dotnet
require_command systemctl
require_command curl

mkdir -p "$APP_ROOT" "$DATA_DIR" "$BACKUP_DIR"

if [ "$SKIP_SERVICE_PATH_CHECK" != "true" ]; then
  service_working_directory="$(systemctl show "$SERVICE_NAME" -p WorkingDirectory --value || true)"
  service_exec_start="$(systemctl show "$SERVICE_NAME" -p ExecStart --value || true)"

  if [ "$service_working_directory" != "$PUBLISH_DIR" ] || [[ "$service_exec_start" != *"$EXPECTED_DLL"* ]]; then
    cat >&2 <<EOF
Refusing to deploy because $SERVICE_NAME does not point at the expected publish directory.

Expected WorkingDirectory:
  $PUBLISH_DIR

Expected ExecStart to include:
  $EXPECTED_DLL

Current WorkingDirectory:
  ${service_working_directory:-<empty>}

Current ExecStart:
  ${service_exec_start:-<empty>}

Update the systemd unit first, then rerun deployment. To bypass this safety
check deliberately, set SKIP_SERVICE_PATH_CHECK=true.
EOF
    exit 1
  fi
fi

if [ ! -d "$REPO_DIR/.git" ]; then
  if [ -e "$REPO_DIR" ] && [ "$(find "$REPO_DIR" -mindepth 1 -maxdepth 1 | wc -l)" -gt 0 ]; then
    printf 'REPO_DIR exists but is not a git checkout: %s\n' "$REPO_DIR" >&2
    printf 'Move it aside or set REPO_DIR to an empty/new checkout path.\n' >&2
    exit 1
  fi

  log "Cloning repository into $REPO_DIR"
  mkdir -p "$(dirname "$REPO_DIR")"
  git clone --branch "$BRANCH" "$REPO_URL" "$REPO_DIR"
fi

cd "$REPO_DIR"

log "Fetching $BRANCH"
git fetch origin "$BRANCH"
git reset --hard "origin/$BRANCH"

if [ -f "$DATA_DIR/minsuxize.db" ]; then
  timestamp="$(date +%Y%m%d-%H%M%S)"
  log "Backing up SQLite database"
  cp "$DATA_DIR/minsuxize.db" "$BACKUP_DIR/minsuxize-$timestamp.db"
fi

log "Restoring and publishing ASP.NET Core app"
dotnet restore "$PROJECT_PATH"
rm -rf "$PUBLISH_DIR"
mkdir -p "$PUBLISH_DIR"

dotnet publish "$PROJECT_PATH" \
  --configuration Release \
  --output "$PUBLISH_DIR" \
  /p:UseAppHost=false

mkdir -p "$PUBLISH_DIR/App_Data"
if [ "$DATA_DIR" != "$PUBLISH_DIR/App_Data" ]; then
  rm -rf "$PUBLISH_DIR/App_Data"
  ln -s "$DATA_DIR" "$PUBLISH_DIR/App_Data"
fi

log "Restarting $SERVICE_NAME"
systemctl restart "$SERVICE_NAME"

log "Waiting for health check: $HEALTH_URL"
for attempt in $(seq 1 30); do
  if curl --silent --fail "$HEALTH_URL" >/dev/null; then
    log "Deployment succeeded"
    exit 0
  fi
  sleep 2
done

log "Health check failed"
systemctl status "$SERVICE_NAME" --no-pager || true
journalctl -u "$SERVICE_NAME" -n 120 --no-pager || true
exit 1
