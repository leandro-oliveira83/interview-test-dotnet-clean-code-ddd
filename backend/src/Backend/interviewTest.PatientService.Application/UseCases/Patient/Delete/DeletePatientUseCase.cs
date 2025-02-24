using AutoMapper;
using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;
using interviewTest.PatientService.Domain.Repositories.Patient;
using openpbl.Shared.Exceptions.ExceptionBase;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace interviewTest.PatientService.Application.UseCases.Patient.Delete;

public class DeletePatientUseCase : IDeletePatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    
    public DeletePatientUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    
    public async Task<ResponseDeletedPatientJson> Execute(RequestDeletePatientJson request)
    {
        await Validate(request);
        
        var success = await _patientRepository.DeleteAsync(request.Id);
        if (!success)
            throw new KeyNotFoundException($"Patient with ID {request.Id} not found");

        await _patientRepository.DeleteAsync(request.Id);
        
        return new ResponseDeletedPatientJson() { Success = true };
    }
    
    private async Task Validate(RequestDeletePatientJson request){
        var validator = new DeletePatientValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false){
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
    
}