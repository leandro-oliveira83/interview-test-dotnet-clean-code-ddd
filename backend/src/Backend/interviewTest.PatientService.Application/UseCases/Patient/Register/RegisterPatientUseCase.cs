using AutoMapper;
using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;
using interviewTest.PatientService.Domain.Repositories.Patient;
using Microsoft.Extensions.Logging;
using openpbl.Shared.Exceptions.ExceptionBase;

namespace interviewTest.PatientService.Application.UseCases.Patient.Register;

public class RegisterPatientUseCase : IRegisterPatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RegisterPatientUseCase> _logger;
    
    public RegisterPatientUseCase(IPatientRepository patientRepository,
        IMapper mapper,
        ILogger<RegisterPatientUseCase> logger)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<ResponseRegisteredPatientJson> Execute(RequestRegisterPatientJson request)
    {
        _logger.LogInformation("Creating a new patient, name: {FirstName}", request.FirstName);
        
        await Validate(request);

        try
        {
        
            var patient = _mapper.Map<Domain.Entities.Patient>(request);
        
            await _patientRepository.CreateAsync(patient);
        
            var response = _mapper.Map<ResponseRegisteredPatientJson>(patient);
        
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Register a new patient  - {Id}", request.FirstName);
            throw new Exception("Failed to register a new patient");
        }

    }
    
    private async Task Validate(RequestRegisterPatientJson request){
        var validator = new RegisterPatientValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false){
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}