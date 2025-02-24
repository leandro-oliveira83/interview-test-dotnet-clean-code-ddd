using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;

namespace interviewTest.PatientService.Application.UseCases.Patient.Delete;

public interface IDeletePatientUseCase
{
    public Task<ResponseDeletedPatientJson> Execute(RequestDeletePatientJson request);
}