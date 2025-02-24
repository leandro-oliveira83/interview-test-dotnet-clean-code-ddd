using FluentValidation;
using interviewTest.PatientService.Communication.Requests;

namespace interviewTest.PatientService.Communication.Validators;

public class RequestDeletePatientValidator: AbstractValidator<RequestDeletePatientJson>
{
    public RequestDeletePatientValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Paitent ID is required");
    }
}