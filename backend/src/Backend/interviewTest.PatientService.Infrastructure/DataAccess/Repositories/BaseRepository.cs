namespace interviewTest.PatientService.Infrastructure.DataAccess.Repositories;

public abstract class BaseRepository<T>
{
    protected readonly InterviewTestDbContext _context;

    protected BaseRepository(InterviewTestDbContext context)
    {
        _context = context;
    }

    public abstract Task<IEnumerable<T>> GetAllAsync();
    public abstract Task<T> GetByIdAsync(Guid id);
    public abstract Task CreateAsync(T entity);
    public abstract Task UpdateAsync(T entity);
    public abstract Task DeleteAsync(Guid id);
}