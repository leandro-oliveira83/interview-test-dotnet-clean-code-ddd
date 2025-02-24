using interviewTest.PatientService.Domain.Entities;
using interviewTest.PatientService.Domain.Repositories.Patient;
using Microsoft.Data.SqlClient;

namespace interviewTest.PatientService.Infrastructure.DataAccess.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly InterviewTestDbContext _dbContext;

    public PatientRepository(InterviewTestDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Patient>> GetAllAsync()
    {
        var patients = new List<Patient>();

        var sql = @"SELECT Id,
                    FirstName,
                    LastName,
                    DateOfBirth,
                    Gender,
                    MaritalStatus,
                    Ethnicity,
                    Race,
                    SocialSecurityNumber,
                    Email,
                    PhoneNumber,
                    AlternatePhoneNumber,
                    AddressLine1,
                    AddressLine2,
                    City,
                    State,
                    ZipCode,
                    Country,
                    CreatedAt,
                    UpdatedAt FROM Patients";

        using var command = new SqlCommand(sql, (SqlConnection)_dbContext.Connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            patients.Add(new Patient
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString(reader.GetOrdinal("LastName")),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
                MaritalStatus = reader.IsDBNull(reader.GetOrdinal("MaritalStatus")) ? null : reader.GetString(reader.GetOrdinal("MaritalStatus")),
                Ethnicity = reader.IsDBNull(reader.GetOrdinal("Ethnicity")) ? null : reader.GetString(reader.GetOrdinal("Ethnicity")),
                Race = reader.IsDBNull(reader.GetOrdinal("Race")) ? null : reader.GetString(reader.GetOrdinal("Race")),
                SocialSecurityNumber = reader.IsDBNull(reader.GetOrdinal("SocialSecurityNumber")) ? null : reader.GetString(reader.GetOrdinal("SocialSecurityNumber")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                AlternatePhoneNumber = reader.IsDBNull(reader.GetOrdinal("AlternatePhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("AlternatePhoneNumber")),
                AddressLine1 = reader.IsDBNull(reader.GetOrdinal("AddressLine1")) ? null : reader.GetString(reader.GetOrdinal("AddressLine1")),
                AddressLine2 = reader.IsDBNull(reader.GetOrdinal("AddressLine2")) ? null : reader.GetString(reader.GetOrdinal("AddressLine2")),
                City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
                ZipCode = reader.IsDBNull(reader.GetOrdinal("ZipCode")) ? null : reader.GetString(reader.GetOrdinal("ZipCode")),
                Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
            });
        }

        return patients;
    }

    public async Task<Patient> GetByIdAsync(Guid id)
    {
        var sql = @"
                SELECT 
                    Id,
                    FirstName,
                    LastName,
                    DateOfBirth,
                    Gender,
                    MaritalStatus,
                    Ethnicity,
                    Race,
                    SocialSecurityNumber,
                    Email,
                    PhoneNumber,
                    AlternatePhoneNumber,
                    AddressLine1,
                    AddressLine2,
                    City,
                    State,
                    ZipCode,
                    Country,
                    CreatedAt,
                    UpdatedAt
                FROM Patients
                WHERE Id = @Id;";

        using var command = new SqlCommand(sql, (SqlConnection)_dbContext.Connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Patient
        {
            Id = reader.GetGuid(reader.GetOrdinal("Id")),
            FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? null : reader.GetString(reader.GetOrdinal("FirstName")),
            LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? null : reader.GetString(reader.GetOrdinal("LastName")),
            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
            Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
            MaritalStatus = reader.IsDBNull(reader.GetOrdinal("MaritalStatus")) ? null : reader.GetString(reader.GetOrdinal("MaritalStatus")),
            Ethnicity = reader.IsDBNull(reader.GetOrdinal("Ethnicity")) ? null : reader.GetString(reader.GetOrdinal("Ethnicity")),
            Race = reader.IsDBNull(reader.GetOrdinal("Race")) ? null : reader.GetString(reader.GetOrdinal("Race")),
            SocialSecurityNumber = reader.IsDBNull(reader.GetOrdinal("SocialSecurityNumber")) ? null : reader.GetString(reader.GetOrdinal("SocialSecurityNumber")),
            Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
            PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
            AlternatePhoneNumber = reader.IsDBNull(reader.GetOrdinal("AlternatePhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("AlternatePhoneNumber")),
            AddressLine1 = reader.IsDBNull(reader.GetOrdinal("AddressLine1")) ? null : reader.GetString(reader.GetOrdinal("AddressLine1")),
            AddressLine2 = reader.IsDBNull(reader.GetOrdinal("AddressLine2")) ? null : reader.GetString(reader.GetOrdinal("AddressLine2")),
            City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
            State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
            ZipCode = reader.IsDBNull(reader.GetOrdinal("ZipCode")) ? null : reader.GetString(reader.GetOrdinal("ZipCode")),
            Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
        };
    }

    public async Task<bool> CreateAsync(Patient patient)
    {
        var sql = @"
        INSERT INTO Patients (
            Id,
            FirstName,
            LastName,
            DateOfBirth,
            Gender,
            MaritalStatus,
            Ethnicity,
            Race,
            SocialSecurityNumber,
            Email,
            PhoneNumber,
            AlternatePhoneNumber,
            AddressLine1,
            AddressLine2,
            City,
            State,
            ZipCode,
            Country,
            CreatedAt,
            UpdatedAt
        )
        VALUES (
            @Id,
            @FirstName,
            @LastName,
            @DateOfBirth,
            @Gender,
            @MaritalStatus,
            @Ethnicity,
            @Race,
            @SocialSecurityNumber,
            @Email,
            @PhoneNumber,
            @AlternatePhoneNumber,
            @AddressLine1,
            @AddressLine2,
            @City,
            @State,
            @ZipCode,
            @Country,
            @CreatedAt,
            @UpdatedAt
        );";

        using var command = new SqlCommand(sql, (SqlConnection)_dbContext.Connection);

        // Adicionando parâmetros com tratamento de nulos
        command.Parameters.AddWithValue("@Id", patient.Id);
        command.Parameters.AddWithValue("@FirstName", patient.FirstName ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@LastName", patient.LastName ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);

        command.Parameters.AddWithValue("@Gender", (object?)patient.Gender ?? DBNull.Value);
        command.Parameters.AddWithValue("@MaritalStatus", (object?)patient.MaritalStatus ?? DBNull.Value);
        command.Parameters.AddWithValue("@Ethnicity", (object?)patient.Ethnicity ?? DBNull.Value);
        command.Parameters.AddWithValue("@Race", (object?)patient.Race ?? DBNull.Value);
        command.Parameters.AddWithValue("@SocialSecurityNumber", (object?)patient.SocialSecurityNumber ?? DBNull.Value);

        command.Parameters.AddWithValue("@Email", (object?)patient.Email ?? DBNull.Value);
        command.Parameters.AddWithValue("@PhoneNumber", (object?)patient.PhoneNumber ?? DBNull.Value);
        command.Parameters.AddWithValue("@AlternatePhoneNumber", (object?)patient.AlternatePhoneNumber ?? DBNull.Value);

        command.Parameters.AddWithValue("@AddressLine1", (object?)patient.AddressLine1 ?? DBNull.Value);
        command.Parameters.AddWithValue("@AddressLine2", (object?)patient.AddressLine2 ?? DBNull.Value);
        command.Parameters.AddWithValue("@City", (object?)patient.City ?? DBNull.Value);
        command.Parameters.AddWithValue("@State", (object?)patient.State ?? DBNull.Value);
        command.Parameters.AddWithValue("@ZipCode", (object?)patient.ZipCode ?? DBNull.Value);
        command.Parameters.AddWithValue("@Country", patient.Country ?? "USA");

        var currentUtcTime = DateTime.UtcNow;
        command.Parameters.AddWithValue("@CreatedAt", currentUtcTime);
        command.Parameters.AddWithValue("@UpdatedAt", currentUtcTime);
        
        var result = await command.ExecuteNonQueryAsync();

        return result == 1 ? true : false;
    }

    public async Task UpdateAsync(Patient patient)
    {
        var sql = @"
            UPDATE Patients
            SET
                FirstName = @FirstName,
                LastName = @LastName,
                DateOfBirth = @DateOfBirth,
                Gender = @Gender,
                MaritalStatus = @MaritalStatus,
                Ethnicity = @Ethnicity,
                Race = @Race,
                SocialSecurityNumber = @SocialSecurityNumber,
                Email = @Email,
                PhoneNumber = @PhoneNumber,
                AlternatePhoneNumber = @AlternatePhoneNumber,
                AddressLine1 = @AddressLine1,
                AddressLine2 = @AddressLine2,
                City = @City,
                State = @State,
                ZipCode = @ZipCode,
                Country = @Country,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;";

        using var command = new SqlCommand(sql, (SqlConnection)_dbContext.Connection);

        // Adiciona os parâmetros com tratamento para valores nulos
        command.Parameters.AddWithValue("@Id", patient.Id);
        command.Parameters.AddWithValue("@FirstName", patient.FirstName ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@LastName", patient.LastName ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);

        command.Parameters.AddWithValue("@Gender", (object?)patient.Gender ?? DBNull.Value);
        command.Parameters.AddWithValue("@MaritalStatus", (object?)patient.MaritalStatus ?? DBNull.Value);
        command.Parameters.AddWithValue("@Ethnicity", (object?)patient.Ethnicity ?? DBNull.Value);
        command.Parameters.AddWithValue("@Race", (object?)patient.Race ?? DBNull.Value);
        command.Parameters.AddWithValue("@SocialSecurityNumber", (object?)patient.SocialSecurityNumber ?? DBNull.Value);

        command.Parameters.AddWithValue("@Email", (object?)patient.Email ?? DBNull.Value);
        command.Parameters.AddWithValue("@PhoneNumber", (object?)patient.PhoneNumber ?? DBNull.Value);
        command.Parameters.AddWithValue("@AlternatePhoneNumber", (object?)patient.AlternatePhoneNumber ?? DBNull.Value);

        command.Parameters.AddWithValue("@AddressLine1", (object?)patient.AddressLine1 ?? DBNull.Value);
        command.Parameters.AddWithValue("@AddressLine2", (object?)patient.AddressLine2 ?? DBNull.Value);
        command.Parameters.AddWithValue("@City", (object?)patient.City ?? DBNull.Value);
        command.Parameters.AddWithValue("@State", (object?)patient.State ?? DBNull.Value);
        command.Parameters.AddWithValue("@ZipCode", (object?)patient.ZipCode ?? DBNull.Value);
        command.Parameters.AddWithValue("@Country", patient.Country ?? "USA");

        command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
        
        var rowsAffected = await command.ExecuteNonQueryAsync();

        if (rowsAffected == 0)
        {
            throw new Exception($"No patient found with Id: {patient.Id}");
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = "DELETE FROM Patients WHERE Id = @Id";

        using var command = new SqlCommand(sql, (SqlConnection)_dbContext.Connection);
        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync();
        return true;
    }
}