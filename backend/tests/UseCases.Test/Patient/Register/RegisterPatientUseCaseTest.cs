using CommonTestUtilities.Logging;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using interviewTest.PatientService.Application.UseCases.Patient.Register;
using Xunit;
using FluentAssertions;
using openpbl.Shared.Exceptions.ExceptionBase;

namespace UseCases.Test.Patient.Register;

public class RegisterPatientUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterPatientJsonBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.FirstName.Should().Be(request.FirstName);
    }
    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterPatientJsonBuilder.Build();
        request.FirstName = string.Empty;

        var useCase = CreateUseCase();

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.GetErrorMessages().Count == 1);
    }
    
    private static RegisterPatientUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var patientRepositoryBuilder = new PatientRepositoryBuilder().Build();
        var logger = new LoggerBuilder<RegisterPatientUseCase>();

        return new RegisterPatientUseCase(patientRepositoryBuilder, mapper, logger.Build());
    }
}