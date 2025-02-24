using AutoMapper;
using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;

namespace interviewTest.PatientService.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterPatientJson, Domain.Entities.Patient>();
        CreateMap<RequestUpdatePatientJson, Domain.Entities.Patient>();
    }

    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.Patient, ResponseRegisteredPatientJson>();
        CreateMap<Domain.Entities.Patient, ResponsePatientProfileJson>();
        CreateMap<Domain.Entities.Patient, ResponseUpdatedPatientJson>();
    }
}