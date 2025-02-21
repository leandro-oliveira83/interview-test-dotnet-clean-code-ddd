using interviewTest.PatientService.API.Common;
using interviewTest.PatientService.Application.UseCases.Patient.Delete;
using interviewTest.PatientService.Application.UseCases.Patient.Get;
using interviewTest.PatientService.Application.UseCases.Patient.Register;
using interviewTest.PatientService.Application.UseCases.Patient.Update;
using interviewTest.PatientService.Communication.Requests;
using interviewTest.PatientService.Communication.Responses;
using interviewTest.PatientService.Communication.Validators;
using Microsoft.AspNetCore.Mvc;

namespace interviewTest.PatientService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController: BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredPatientJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterPatientUseCase useCase,
        [FromBody] RequestRegisterPatientJson request)
    {
        var validator = new RequestRegisterPatientValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var result = await useCase.Execute(request);

        return Created(string.Empty, new ApiResponseWithData<ResponseRegisteredPatientJson>
        {
            Success = true,
            Message = "Patient created successfully",
            Data = result
        });
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<ResponsePatientProfileJson>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromServices] IGetPatientUseCase useCase)
    {
        var result = await useCase.Execute();
        
        return Ok(new ApiResponseWithData<List<ResponsePatientProfileJson>>
        {
            Success = true,
            Message = "Patients retrieved successfully",
            Data = result
        });

        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<ResponseUpdatedPatientJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdatePatientUseCase useCase, 
        [FromBody] RequestUpdatePatientJson request)
    {
        var validator = new RequestUpdatePatientValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var result = await useCase.Execute(request);

        return Created(string.Empty, new ApiResponseWithData<ResponseUpdatedPatientJson>
        {
            Success = true,
            Message = "Patient updated successfully",
            Data = result
        });
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeletePatientUseCase useCase, [FromRoute] Guid id)
    {
        var request = new RequestDeletePatientJson() { Id = id };
        var validator = new RequestDeletePatientValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        await useCase.Execute(request);
        
        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Patient deleted successfully"
        });
    }

}