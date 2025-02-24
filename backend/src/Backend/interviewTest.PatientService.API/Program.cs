using interviewTest.PatientService.API.Filters;
using interviewTest.PatientService.Application;
using interviewTest.PatientService.Infrastructure;
using interviewTest.PatientService.Infrastructure.Extensions;
using interviewTest.PatientService.Infrastructure.HealthChecks;
using interviewTest.PatientService.Infrastructure.Logging;
using interviewTest.PatientService.Infrastructure.Migrations;
using Serilog;

Log.Information("Starting web application");

var builder = WebApplication.CreateBuilder(args);

builder.AddDefaultLogging();

builder.Services.AddEndpointsApiExplorer();
builder.AddBasicHealthChecks();

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var baseUrl = builder.Configuration.GetValue<string>("FrontEnd:BaseUrl");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins(baseUrl) 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); 
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");

app.UseHttpsRedirection();

app.UseBasicHealthChecks();

app.MapControllers();

MigrateDatabase();

app.Run();

// run migrate database
void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnviroment())
        return;

    var connectionString = builder.Configuration.ConnetionString();

    // create scopre for dependency injection
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DatabaseMigration.Migrate(connectionString, serviceScope.ServiceProvider);
}

