# Aliyun ECS Systemd Deployment

This project can be deployed to an existing Alibaba Cloud Linux server without
Docker. The current server shape is:

- Alibaba Cloud Linux 3
- `nginx` installed and running
- `git` installed
- `dotnet` installed
- `minsuxize.service` running through systemd
- app root appears to be `/var/www/minsuxize`
- `minsuzhi.cn` / `www.minsuzhi.cn` proxies to `http://localhost:5000`
- `xrcraft.cn` is configured in separate Nginx server blocks

## Server Requirements

The server needs:

```bash
git --version
dotnet --info
systemctl status minsuxize.service
curl http://127.0.0.1:5000/healthz
```

If the service listens on a different local port, set `ALIYUN_HEALTH_URL` in
GitHub Secrets.

## GitHub Secrets

Add these secrets to the GitHub repository:

```text
ALIYUN_HOST          server public IP or domain
ALIYUN_USER          SSH user, for example root or a deploy user
ALIYUN_SSH_KEY       private key content for SSH login
ALIYUN_SSH_PORT      SSH port, usually 22
```

Optional secrets:

```text
ALIYUN_APP_ROOT      default: /var/www/minsuxize
ALIYUN_REPO_DIR      default: /var/www/minsuxize/repo
ALIYUN_SERVICE_NAME  default: minsuxize.service
ALIYUN_HEALTH_URL    default: http://127.0.0.1:5000/healthz
```

## First Server Check

Before enabling automatic deployment, confirm the service paths:

```bash
systemctl cat minsuxize.service
systemctl status minsuxize.service --no-pager
ls -la /var/www/minsuxize
```

The recommended layout is:

```text
/var/www/minsuxize/
  repo/       git checkout
  publish/    dotnet publish output; systemd should run from here
  App_Data/   persistent SQLite database
  backups/    SQLite backups made before deployment
```

Your current unit may still point directly at `/var/www/minsuxize`. In that
case, update the systemd unit once before enabling automatic deployment. The
deploy script refuses to run when `WorkingDirectory` and `ExecStart` do not
point at the expected publish directory, so it will not silently deploy to the
wrong place.

## One-Time Bootstrap

Your current production unit runs directly from:

```text
WorkingDirectory=/var/www/minsuxize
ExecStart=/usr/share/dotnet/dotnet /var/www/minsuxize/MinsuXize.Web.dll
```

Before switching systemd to `/var/www/minsuxize/publish`, create the first
publish output once:

```bash
mkdir -p /var/www/minsuxize/repo /var/www/minsuxize/publish /var/www/minsuxize/App_Data /var/www/minsuxize/backups
chown -R ecs-assist-user:ecs-assist-user /var/www/minsuxize

if [ ! -d /var/www/minsuxize/repo/.git ]; then
  sudo -u ecs-assist-user git clone --branch main https://github.com/TrisomyManager/minsuxize.git /var/www/minsuxize/repo
fi

cd /var/www/minsuxize/repo
sudo -u ecs-assist-user git fetch origin main
sudo -u ecs-assist-user git reset --hard origin/main
sudo -u ecs-assist-user dotnet publish src/MinsuXize.Web/MinsuXize.Web.csproj \
  --configuration Release \
  --output /var/www/minsuxize/publish \
  /p:UseAppHost=false

rm -rf /var/www/minsuxize/publish/App_Data
ln -s /var/www/minsuxize/App_Data /var/www/minsuxize/publish/App_Data
chown -h ecs-assist-user:ecs-assist-user /var/www/minsuxize/publish/App_Data
```

Recommended unit shape:

```ini
[Unit]
Description=MinsuXize Web Application
After=network.target

[Service]
WorkingDirectory=/var/www/minsuxize/publish
ExecStart=/usr/bin/dotnet /var/www/minsuxize/publish/MinsuXize.Web.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=minsuxize
User=ecs-assist-user
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://127.0.0.1:5000
Environment=DOTNET_CLI_TELEMETRY_OPTOUT=1
Environment=ConnectionStrings__DefaultConnection=Data Source=/var/www/minsuxize/App_Data/minsuxize.db

[Install]
WantedBy=multi-user.target
```

Apply the update:

```bash
sudo systemctl edit --full minsuxize.service
sudo systemctl daemon-reload
sudo systemctl restart minsuxize.service
sudo systemctl status minsuxize.service --no-pager
curl --fail http://127.0.0.1:5000/healthz
```

This change only affects `minsuxize.service`. It does not restart `nginx`, does
not edit `/etc/nginx/conf.d/*`, and does not touch other web roots under
`/var/www`.

## Manual Deploy

On the server:

```bash
cd /var/www/minsuxize
bash repo/scripts/deploy_aliyun_systemd.sh
```

Useful overrides:

```bash
APP_ROOT=/var/www/minsuxize \
SERVICE_NAME=minsuxize.service \
HEALTH_URL=http://127.0.0.1:5000/healthz \
bash /var/www/minsuxize/repo/scripts/deploy_aliyun_systemd.sh
```

## Automatic Deploy

The workflow `.github/workflows/deploy-aliyun-systemd.yml` runs on pushes to
`main` and can also be triggered manually from GitHub Actions.

Deployment flow:

```text
build Release
run public site checks
SSH to Aliyun ECS
git fetch/reset to the pushed branch
backup App_Data/minsuxize.db
dotnet publish
restart minsuxize.service
check /healthz
```

## Rollback

The deploy script backs up the SQLite database into:

```text
/var/www/minsuxize/backups/
```

Code rollback is done by reverting or resetting the repository to an earlier
commit and rerunning:

```bash
bash scripts/deploy_aliyun_systemd.sh
```
