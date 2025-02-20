using interviewTest.PatientService.Application.UseCases.Patient.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace interviewTest.PatientService.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
    }
    
    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterPatientUseCase, RegisterPatientUseCase>();
    }
}