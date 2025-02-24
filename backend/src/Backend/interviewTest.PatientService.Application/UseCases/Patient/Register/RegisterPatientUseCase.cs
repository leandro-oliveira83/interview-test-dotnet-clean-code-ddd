using AutoMapper;
using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;
using interviewTest.PatientService.Domain.Repositories.Patient;
using openpbl.Shared.Exceptions.ExceptionBase;

namespace interviewTest.PatientService.Application.UseCases.Patient.Register;

public class RegisterPatientUseCase : IRegisterPatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    
    public RegisterPatientUseCase(IPatientRepository patientRepository,
        IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseRegisteredPatientJson> Execute(RequestRegisterPatientJson request)
    {
        await Validate(request);
        
        var patient = _mapper.Map<Domain.Entities.Patient>(request);
        
        await _patientRepository.CreateAsync(patient);
        
        var response = _mapper.Map<ResponseRegisteredPatientJson>(patient);
        
        return response;
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