using Microsoft.Extensions.Configuration;

namespace interviewTest.PatientService.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static string ConnetionString(this IConfiguration configurarion)
    {
        return configurarion.GetConnectionString("Connection")!;
    }

    public static bool IsUnitTestEnviroment(this IConfiguration configurarion)
    {
        return configurarion.GetValue<bool>("InMemoryTest");
    }
}