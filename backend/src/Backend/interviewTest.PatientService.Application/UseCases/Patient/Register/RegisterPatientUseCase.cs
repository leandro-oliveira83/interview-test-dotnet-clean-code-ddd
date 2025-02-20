using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;

namespace interviewTest.PatientService.Application.UseCases.Patient.Register;

public class RegisterPatientUseCase : IRegisterPatientUseCase
{
    public RegisterPatientUseCase()
    {
    }
    
    public async Task<ResponseRegisteredPatientJson> Execute(RequestRegisterPatientJson request)
    {
        return new ResponseRegisteredPatientJson
        {
            FirstName = "Leandro"
        };
    }
}