using FluentValidation;
using interviewTest.PatientService.Communication.Requests;

namespace interviewTest.PatientService.Communication.Validators;

public class RequestRegisterPatientValidator: AbstractValidator<RequestRegisterPatientJson>
{
    public RequestRegisterPatientValidator()
    {
        RuleFor(patient => patient.FirstName).NotEmpty();
        RuleFor(patient => patient.LastName).NotEmpty();
        RuleFor(patient => patient.DateOfBirth).NotEmpty();
        RuleFor(patient => patient.Gender).NotEmpty();
        RuleFor(patient => patient.MaritalStatus).NotEmpty();
        RuleFor(patient => patient.Ethnicity).NotEmpty();
        RuleFor(patient => patient.Race).NotEmpty();
        RuleFor(patient => patient.SocialSecurityNumber).NotEmpty();
        RuleFor(patient => patient.Email).NotEmpty();

        When(patient => string.IsNullOrEmpty(patient.Email) == false, () =>
        {
            RuleFor(patient => patient.Email).EmailAddress();
        });
        
        RuleFor(patient => patient.PhoneNumber).NotEmpty();
        RuleFor(patient => patient.AlternatePhoneNumber).NotEmpty();
        RuleFor(patient => patient.AddressLine1).NotEmpty();
        RuleFor(patient => patient.AddressLine2).NotEmpty();
        RuleFor(patient => patient.City).NotEmpty();
        RuleFor(patient => patient.State).NotEmpty().MaximumLength(2);
        RuleFor(patient => patient.ZipCode).NotEmpty();
        RuleFor(patient => patient.Country).NotEmpty();
        
    }
}