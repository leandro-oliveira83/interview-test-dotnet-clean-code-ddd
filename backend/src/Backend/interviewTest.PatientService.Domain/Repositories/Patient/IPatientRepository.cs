namespace interviewTest.PatientService.Domain.Repositories.Patient;

public interface IPatientRepository
{
    public Task<List<Entities.Patient>> GetAllAsync();
    public Task<Entities.Patient> GetByIdAsync(Guid id);
    public Task<bool> CreateAsync(Entities.Patient entity);
    public Task UpdateAsync(Entities.Patient entity);
    public Task<bool> DeleteAsync(Guid id);
}