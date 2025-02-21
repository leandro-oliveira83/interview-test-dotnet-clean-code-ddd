using FluentValidation;
using interviewTest.PatientService.Communication.Requests;

namespace interviewTest.PatientService.Application.UseCases.Patient.Delete;

public class DeletePatientValidator: AbstractValidator<RequestDeletePatientJson>
{
    public DeletePatientValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Patient ID is required");
    }
}
