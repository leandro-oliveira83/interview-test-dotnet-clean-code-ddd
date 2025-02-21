using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace interviewTest.PatientService.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider serviceProvider){
        EnsureDatabaseCreated(connectionString);
        
        // run migrate
        MigrationDatabase(serviceProvider);
    }
    
    private async static void EnsureDatabaseCreated(string connectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.InitialCatalog;

        // Remove a reference to the database to connect to the server
        connectionStringBuilder.Remove("Initial Catalog");

        using var conn = new SqlConnection(connectionStringBuilder.ConnectionString);
        await conn.OpenAsync();

        // Checks if the database exists
        var checkDatabaseQuery = "SELECT 1 FROM sys.databases WHERE name = @DatabaseName";
        using (var command = new SqlCommand(checkDatabaseQuery, conn))
        {
            command.Parameters.AddWithValue("@DatabaseName", databaseName); // Prevent SQL Injection

            var databaseExists = await command.ExecuteScalarAsync() != null;

            if (!databaseExists)
            {
                var createDatabaseQuery = $"CREATE DATABASE [{databaseName}]"; 
                using var createCommand = new SqlCommand(createDatabaseQuery, conn);
                await createCommand.ExecuteNonQueryAsync();
            }
        }
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();

        runner.MigrateUp();
    }
}