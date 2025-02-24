namespace interviewTest.PatientService.Communication.Requests;

public class RequestRegisterPatientJson
{
    public string FirstName { get; set; }  = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;
    public string Ethnicity { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string SocialSecurityNumber { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AlternatePhoneNumber { get; set; } = string.Empty;
    
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}