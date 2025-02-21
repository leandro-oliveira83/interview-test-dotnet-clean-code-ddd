using AutoMapper;
using interviewTest.PatientService.Communication.Responses;
using interviewTest.PatientService.Domain.Repositories.Patient;

namespace interviewTest.PatientService.Application.UseCases.Patient.Get;

public class GetPatientUseCase : IGetPatientUseCase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    
    public GetPatientUseCase(IPatientRepository patientRepository,
        IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }
    
    public async Task<List<ResponsePatientProfileJson>> Execute()
    {
        var patients = await _patientRepository.GetAllAsync();
        
        var response = _mapper.Map<List<ResponsePatientProfileJson>>(patients);
        
        return response;
    }
}