namespace interviewTest.PatientService.Domain.Entities;

public class Patient : EntityBase
{
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string Ethnicity { get; set; }
    public string Race { get; set; }
    public string SocialSecurityNumber { get; set; }
    
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AlternatePhoneNumber { get; set; }
    
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; } = "USA";
}