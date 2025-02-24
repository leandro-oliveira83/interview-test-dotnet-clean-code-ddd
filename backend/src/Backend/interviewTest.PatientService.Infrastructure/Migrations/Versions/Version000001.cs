using FluentMigrator;

namespace interviewTest.PatientService.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_PATIENT, "Create table to save the user's information")]
public class Version000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Patients")
            .WithColumn("FirstName").AsString(100).NotNullable()
            .WithColumn("LastName").AsString(100).NotNullable()
            .WithColumn("DateOfBirth").AsDate().NotNullable()
            .WithColumn("Gender").AsString(20).Nullable()
            .WithColumn("MaritalStatus").AsString(20).Nullable()
            .WithColumn("Ethnicity").AsString(50).Nullable()
            .WithColumn("Race").AsString(50).Nullable()
            .WithColumn("SocialSecurityNumber").AsFixedLengthString(11).Nullable()

            .WithColumn("Email").AsString(200).Nullable()
            .WithColumn("PhoneNumber").AsString(20).Nullable()
            .WithColumn("AlternatePhoneNumber").AsString(20).Nullable()

            .WithColumn("AddressLine1").AsString(200).Nullable()
            .WithColumn("AddressLine2").AsString(200).Nullable()
            .WithColumn("City").AsString(100).Nullable()
            .WithColumn("State").AsFixedLengthString(2).Nullable()
            .WithColumn("ZipCode").AsFixedLengthString(10).Nullable()
            .WithColumn("Country").AsString(50).NotNullable().WithDefaultValue("USA");
    }
}