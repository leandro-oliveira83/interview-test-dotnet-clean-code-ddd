using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Sweden;
using interviewTest.PatientService.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class PatientBuilder
{
    public static List<Patient> Collection(uint count = 2)
    {
        var list = new List<Patient>();

        if (count == 0)
            count = 1;

        for (int i = 0; i < count; i++)
        {
            var fakePatient = Build();
            list.Add(fakePatient);
        }

        return list;
    }
    
    public static Patient Build()
    {
        return new Faker<Patient>()
            .RuleFor(patient => patient.Id, Guid.NewGuid())
            .RuleFor(patient => patient.FirstName, (f) => f.Person.FirstName)
            .RuleFor(patient => patient.LastName, (f) => f.Person.LastName)
            .RuleFor(patient => patient.DateOfBirth, (f) => f.Person.DateOfBirth)
            .RuleFor(patient => patient.Gender, (f) => f.Person.Gender == Name.Gender.Female ? "female" : "male")
            .RuleFor(patient => patient.MaritalStatus, "single")
            .RuleFor(patient => patient.Ethnicity, "hispanic-american")
            .RuleFor(patient => patient.Race, "white")
            .RuleFor(patient => patient.SocialSecurityNumber, (f) => f.Person.Personnummer())
            .RuleFor(patient => patient.Email, (f, user) => f.Internet.Email(user.FirstName))
            .RuleFor(patient => patient.PhoneNumber, (f) => f.Person.Phone)
            .RuleFor(patient => patient.AlternatePhoneNumber, (f) => f.Person.Phone)
            .RuleFor(patient => patient.AddressLine1, (f) => f.Person.Address.Street)
            .RuleFor(patient => patient.AddressLine2, (f) => f.Person.Address.Suite)
            .RuleFor(patient => patient.City, (f) => f.Person.Address.City)
            .RuleFor(patient => patient.State, (f) => "NY")
            .RuleFor(patient => patient.ZipCode, (f) => f.Person.Address.ZipCode)
            .RuleFor(patient => patient.Country, (f) => f.Address.Country());
    } 
}