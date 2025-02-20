namespace interviewTest.PatientService.Communication.Requests;

public class RequestRegisterPatientJson
{
    public string FirstName { get; set; }  = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Suffix { get; set; } = string.Empty;
}