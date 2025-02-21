using interviewTest.PatientService.Communication.Responses;

namespace interviewTest.PatientService.Application.UseCases.Patient.Get;

public interface IGetPatientUseCase
{
    public Task<List<ResponsePatientProfileJson>> Execute();
}