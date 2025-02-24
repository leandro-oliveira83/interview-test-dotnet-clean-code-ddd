using System.Data;
using System.Reflection;
using FluentMigrator.Runner;
using interviewTest.PatientService.Domain.Repositories.Patient;
using interviewTest.PatientService.Infrastructure.DataAccess;
using interviewTest.PatientService.Infrastructure.DataAccess.Repositories;
using interviewTest.PatientService.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace interviewTest.PatientService.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddFluentMigrator(services, configuration);
    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnetionString();
        
        services.AddScoped<IDbConnection>(sp =>
            new SqlConnection(connectionString));
        
        services.AddScoped<InterviewTestDbContext>();
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IPatientRepository, PatientRepository>();
    }
    
    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnetionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("interviewTest.PatientService.Infrastructure")).For.All();
        });
    }
}