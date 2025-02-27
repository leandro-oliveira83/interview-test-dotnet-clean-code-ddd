using AutoMapper;
using interviewTest.PatientService.Communication.Responses;
using interviewTest.PatientService.Domain.Repositories.Patient;
using Microsoft.Extensions.Logging;

namespace interviewTest.PatientService.Application.UseCases.Patient.Get;

public class GetPatientUseCase : IGetPatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPatientUseCase> _logger;
    
    public GetPatientUseCase(IPatientRepository patientRepository,
        IMapper mapper,
        ILogger<GetPatientUseCase> logger)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<ResponsePatientProfileJson>> Execute()
    {
        _logger.LogInformation("Retrieve all patients");

        try
        {
            var patients = await _patientRepository.GetAllAsync();
        
            var response = _mapper.Map<List<ResponsePatientProfileJson>>(patients);
        
            return response; 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Retrieve all patients");
            throw new Exception("Failed to retrieve all patients");
        }

    }
}