using System.Data;
using Microsoft.Data.SqlClient;

namespace interviewTest.PatientService.Infrastructure.DataAccess;

public class InterviewTestDbContext : IDisposable
{
    private readonly IDbConnection _connection;

    public InterviewTestDbContext(IDbConnection connection)
    {
        _connection = connection;
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
    }

    public IDbConnection Connection => _connection;

    public void Dispose()
    {
        if (_connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
        _connection.Dispose();
    }
}