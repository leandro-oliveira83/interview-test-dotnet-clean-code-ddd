using AutoMapper;
using interviewTest.PatientService.Application.Services.AutoMapper;

namespace CommonTestUtilities.Mapper;

public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        return mapper;
    }
}