namespace interviewTest.PatientService.Communication.Responses;

public class ResponseRegisteredPatientJson
{
    public Guid Id { get; set; }
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
}