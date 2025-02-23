using Bogus;
using interviewTest.PatientService.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestDeletePatientJsonBuilder
{
    public static RequestDeletePatientJson Build()
    {
        return new Faker<RequestDeletePatientJson>()
            .RuleFor(patient => patient.Id, Guid.Empty);

    }
}