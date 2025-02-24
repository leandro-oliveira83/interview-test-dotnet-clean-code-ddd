using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;

namespace interviewTest.PatientService.Application.UseCases.Patient.Register;

public interface IRegisterPatientUseCase
{
    public Task<ResponseRegisteredPatientJson> Execute(RequestRegisterPatientJson request);
}