using AutoMapper;
using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Domain.Repositories.Patient;
using Microsoft.Extensions.Logging;
using openpbl.Shared.Exceptions.ExceptionBase;

namespace interviewTest.PatientService.Application.UseCases.Patient.Update;

public class UpdatePatientUseCase : IUpdatePatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdatePatientUseCase> _logger;
    
    public UpdatePatientUseCase(IPatientRepository patientRepository,
        IMapper mapper,
        ILogger<UpdatePatientUseCase> logger)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<ResponseUpdatedPatientJson> Execute(RequestUpdatePatientJson request)
    {
        _logger.LogInformation("Editing a patient, id: {id}", request.Id);
        
        await Validate(request);

        try
        {
        
            var patient = _mapper.Map<Domain.Entities.Patient>(request);
        
            var existingPatient = await _patientRepository.GetByIdAsync(patient.Id);
            if (existingPatient == null)
            {
                throw new KeyNotFoundException("The patient not found");
            }
        
            await _patientRepository.UpdateAsync(patient);
        
            var response = _mapper.Map<ResponseUpdatedPatientJson>(patient);
        
            return response; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update a patient - {Id}", request.Id);
            throw new Exception(ex.Message);
        }
        
    }
    
    private async Task Validate(RequestUpdatePatientJson request){
        var validator = new UpdatePatientValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false){
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}