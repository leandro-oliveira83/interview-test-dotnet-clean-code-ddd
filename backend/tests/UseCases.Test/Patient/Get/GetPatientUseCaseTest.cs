using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using interviewTest.PatientService.Application.UseCases.Patient.Get;
using Xunit;

namespace UseCases.Test.Patient.Get;

public class GetPatientUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var patients = PatientBuilder.Collection();

        var useCase = CreateUseCase(patients);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Should()
            .HaveCountGreaterThan(0)
            .And.OnlyHaveUniqueItems(patient => patient.Id);
    }

    private static GetPatientUseCase CreateUseCase(List<interviewTest.PatientService.Domain.Entities.Patient> patients)
    {
        var mapper = MapperBuilder.Build();
        var patientRepositoryBuilder = new PatientRepositoryBuilder().GetAllAsync(patients).Build();

        return new GetPatientUseCase(patientRepositoryBuilder, mapper);
    }
}