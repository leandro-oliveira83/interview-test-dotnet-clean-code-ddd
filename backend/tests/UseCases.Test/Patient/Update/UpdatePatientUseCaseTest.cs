using CommonTestUtilities.Entities;
using CommonTestUtilities.Logging;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using interviewTest.PatientService.Application.UseCases.Patient.Update;
using openpbl.Shared.Exceptions.ExceptionBase;
using Xunit;

namespace UseCases.Test.Patient.Update;

public class UpdatePatientUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var patient = PatientBuilder.Build();

        var request = RequestUpdatePatientJsonBuilder.Build();
        request.Id = patient.Id;

        var useCase = CreateUseCase(patient);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
        
        patient.Id.Should().Be(request.Id);
    }
    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var patient = PatientBuilder.Build();
        
        var request = RequestUpdatePatientJsonBuilder.Build();
        request.FirstName = string.Empty;

        var useCase = CreateUseCase(patient);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1);
    }
    
    private static UpdatePatientUseCase CreateUseCase(interviewTest.PatientService.Domain.Entities.Patient patient)
    {
        var mapper = MapperBuilder.Build();
        var patientRepositoryBuilder = new PatientRepositoryBuilder().GetByIdAsync(patient).Build();
        var logger = new LoggerBuilder<UpdatePatientUseCase>();

        return new UpdatePatientUseCase(patientRepositoryBuilder, mapper, logger.Build());
    }
}