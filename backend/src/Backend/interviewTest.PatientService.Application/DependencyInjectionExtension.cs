using interviewTest.PatientService.Application.Services.AutoMapper;
using interviewTest.PatientService.Application.UseCases.Patient.Delete;
using interviewTest.PatientService.Application.UseCases.Patient.Get;
using interviewTest.PatientService.Application.UseCases.Patient.Register;
using interviewTest.PatientService.Application.UseCases.Patient.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace interviewTest.PatientService.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }
    
    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
        {
            autoMapperOptions.AddProfile(new AutoMapping());
        }).CreateMapper());
    }
    
    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterPatientUseCase, RegisterPatientUseCase>();
        services.AddScoped<IGetPatientUseCase, GetPatientUseCase>();
        services.AddScoped<IUpdatePatientUseCase, UpdatePatientUseCase>();
        services.AddScoped<IDeletePatientUseCase, DeletePatientUseCase>();
    }
}