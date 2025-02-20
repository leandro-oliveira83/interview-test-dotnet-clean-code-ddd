using interviewTest.PatientService.Application.UseCases.Patient.Register;
using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace interviewTest.PatientService.API.Controllers;

[Route("[controller]")]
[ApiController]
public class PatientController: ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredPatientJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromServices] IRegisterPatientUseCase useCase,
        [FromBody] RequestRegisterPatientJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}