using interviewTest.PatientService.Domain.Entities;
using interviewTest.PatientService.Domain.Repositories.Patient;
using Moq;

namespace CommonTestUtilities.Repositories;

public class PatientRepositoryBuilder
{
    private readonly Mock<IPatientRepository> _repository;
    
    public PatientRepositoryBuilder() => _repository = new Mock<IPatientRepository>();
    
    public PatientRepositoryBuilder GetByIdAsync(Patient patient)
    {
        _repository.Setup(x => x.GetByIdAsync(patient.Id)).ReturnsAsync(patient);
        return this;
    }
    
    public PatientRepositoryBuilder DeleteAsync(Patient patient)
    {
        _repository.Setup(x => x.DeleteAsync(patient.Id)).ReturnsAsync(true);
        return this;
    }
    
    public PatientRepositoryBuilder GetAllAsync(List<Patient> patients)
    {
        _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(patients);
        return this;
    }
    public IPatientRepository Build() => _repository.Object;
}
