using AutoMapper;
using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;
using interviewTest.PatientService.Domain.Repositories.Patient;
using Microsoft.Extensions.Logging;
using openpbl.Shared.Exceptions.ExceptionBase;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace interviewTest.PatientService.Application.UseCases.Patient.Delete;

public class DeletePatientUseCase : IDeletePatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly ILogger<DeletePatientUseCase> _logger;
    
    public DeletePatientUseCase(IPatientRepository patientRepository,
        ILogger<DeletePatientUseCase> logger)
    {
        _patientRepository = patientRepository;
        _logger = logger;
    }
    
    public async Task<ResponseDeletedPatientJson> Execute(RequestDeletePatientJson request)
    {
        _logger.LogInformation("Remove a patient, id: {id}", request.Id);

        try
        {
            await Validate(request);
        
            var success = await _patientRepository.DeleteAsync(request.Id);
            if (!success)
                throw new KeyNotFoundException($"Patient with ID {request.Id} not found");

            await _patientRepository.DeleteAsync(request.Id);
        
            return new ResponseDeletedPatientJson() { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete a patient - {Id}", request.Id);
            throw new Exception("Failed to delete patient");
        }

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