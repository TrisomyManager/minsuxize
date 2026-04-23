using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MinsuXize.Web.Data;

public static class DbSchemaMigrator
{
#pragma warning disable EF1002
    public static void EnsureKnowledgeSchema(AppDbContext context)
    {
        EnsureColumn(context, "Regions", "Slug", "TEXT NOT NULL DEFAULT ''");
        EnsureColumn(context, "Regions", "UpdatedAt", "TEXT NOT NULL DEFAULT '1970-01-01T00:00:00Z'");

        EnsureColumn(context, "Festivals", "Slug", "TEXT NOT NULL DEFAULT ''");
        EnsureColumn(context, "Festivals", "UpdatedAt", "TEXT NOT NULL DEFAULT '1970-01-01T00:00:00Z'");

        EnsureColumn(context, "Entries", "Slug", "TEXT NOT NULL DEFAULT ''");
        EnsureColumn(context, "Entries", "ContentType", "TEXT NOT NULL DEFAULT 'ritual'");
        EnsureColumn(context, "Entries", "RegionFieldsJson", "TEXT NOT NULL DEFAULT '{}'");
        EnsureColumn(context, "Entries", "PreparationsJson", "TEXT NOT NULL DEFAULT '[]'");
        EnsureColumn(context, "Entries", "RegionalDifferencesJson", "TEXT NOT NULL DEFAULT '[]'");
        EnsureColumn(context, "Entries", "TagsJson", "TEXT NOT NULL DEFAULT '[]'");
        EnsureColumn(context, "Entries", "ReviewStatus", "TEXT NOT NULL DEFAULT 'pending-verification'");
        EnsureColumn(context, "Entries", "ConfidenceLevel", "TEXT NOT NULL DEFAULT 'medium'");
        EnsureColumn(context, "Entries", "SourceGrade", "TEXT NOT NULL DEFAULT 'unverified'");

        EnsureColumn(context, "Sources", "SourceLevel", "TEXT NOT NULL DEFAULT 'unverified'");

        EnsureColumn(context, "Submissions", "RelatedEntryId", "INTEGER NULL");
        EnsureColumn(context, "Submissions", "ContentType", "TEXT NOT NULL DEFAULT 'ritual'");

        context.Database.ExecuteSqlRaw("""
            CREATE TABLE IF NOT EXISTS EntryFaqs (
                Id INTEGER NOT NULL CONSTRAINT PK_EntryFaqs PRIMARY KEY AUTOINCREMENT,
                EntryId INTEGER NOT NULL,
                Question TEXT NOT NULL,
                Answer TEXT NOT NULL,
                SortOrder INTEGER NOT NULL DEFAULT 0
            );
            """);

        context.Database.ExecuteSqlRaw("""
            CREATE TABLE IF NOT EXISTS EntryRelations (
                Id INTEGER NOT NULL CONSTRAINT PK_EntryRelations PRIMARY KEY AUTOINCREMENT,
                EntryId INTEGER NOT NULL,
                RelatedEntryId INTEGER NOT NULL,
                RelationType TEXT NOT NULL DEFAULT 'related',
                Note TEXT NOT NULL DEFAULT ''
            );
            """);

        EnsureIndex(context, "IX_Regions_Slug", "Regions", "Slug", unique: false);
        EnsureIndex(context, "IX_Festivals_Slug", "Festivals", "Slug", unique: false);
        EnsureIndex(context, "IX_Entries_Slug", "Entries", "Slug", unique: false);
        EnsureIndex(context, "IX_Entries_ContentType", "Entries", "ContentType", unique: false);
        EnsureIndex(context, "IX_EntryFaqs_EntryId_SortOrder", "EntryFaqs", "EntryId, SortOrder", unique: false);
        EnsureIndex(context, "IX_EntryRelations_EntryId_RelatedEntryId", "EntryRelations", "EntryId, RelatedEntryId", unique: true);
    }

    private static void EnsureColumn(AppDbContext context, string table, string column, string definition)
    {
        if (ColumnExists(context, table, column))
        {
            return;
        }

        ExecuteNonQuery(context, $"ALTER TABLE {table} ADD COLUMN {column} {definition};");
    }

    private static bool ColumnExists(AppDbContext context, string table, string column)
    {
        var connection = context.Database.GetDbConnection();
        var closeConnection = connection.State != System.Data.ConnectionState.Open;
        if (closeConnection)
        {
            connection.Open();
        }

        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"PRAGMA table_info({table});";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (string.Equals(reader.GetString(1), column, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }
        finally
        {
            if (closeConnection)
            {
                connection.Close();
            }
        }

        return false;
    }

    private static void EnsureIndex(AppDbContext context, string indexName, string table, string columns, bool unique)
    {
        var uniqueSql = unique ? "UNIQUE " : string.Empty;
        ExecuteNonQuery(context, $"CREATE {uniqueSql}INDEX IF NOT EXISTS {indexName} ON {table} ({columns});");
    }

    private static void ExecuteNonQuery(AppDbContext context, string sql)
    {
        var connection = context.Database.GetDbConnection();
        var closeConnection = connection.State != System.Data.ConnectionState.Open;
        if (closeConnection)
        {
            connection.Open();
        }

        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
        finally
        {
            if (closeConnection)
            {
                connection.Close();
            }
        }
    }
#pragma warning restore EF1002
}
