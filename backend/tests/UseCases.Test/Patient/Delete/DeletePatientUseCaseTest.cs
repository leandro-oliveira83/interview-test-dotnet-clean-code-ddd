using CommonTestUtilities.Entities;
using CommonTestUtilities.Logging;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using interviewTest.PatientService.Application.UseCases.Patient.Delete;
using Xunit;

namespace UseCases.Test.Patient.Delete;

public class DeletePatientUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var patient = PatientBuilder.Build();
        
        var request = RequestDeletePatientJsonBuilder.Build();
        request.Id = patient.Id;

        var useCase = CreateUseCase(patient);

        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    private static DeletePatientUseCase CreateUseCase(interviewTest.PatientService.Domain.Entities.Patient patient)
    {
        var patientRepositoryBuilder = new PatientRepositoryBuilder().DeleteAsync(patient).Build();
        var logger = new LoggerBuilder<DeletePatientUseCase>();

        return new DeletePatientUseCase(patientRepositoryBuilder, logger.Build());
    }
}