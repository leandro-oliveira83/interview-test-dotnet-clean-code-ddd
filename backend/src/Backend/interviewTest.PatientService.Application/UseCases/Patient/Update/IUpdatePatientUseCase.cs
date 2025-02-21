using interviewTest.PatientService.Communication.Requests;

namespace interviewTest.PatientService.Application.UseCases.Patient.Update;

public interface IUpdatePatientUseCase
{
    public Task<ResponseUpdatedPatientJson> Execute(RequestUpdatePatientJson request);
}