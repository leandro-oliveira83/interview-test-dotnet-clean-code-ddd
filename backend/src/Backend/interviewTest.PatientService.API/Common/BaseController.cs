using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace interviewTest.PatientService.API.Common;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult Ok(string message) =>
        base.Ok(new ApiResponse() { Success = true, Message = message});
    
    protected IActionResult Ok<T>(T data, string message) =>
        base.Ok(new ApiResponseWithData<T> { Data = data, Success = true, Message = message});

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse { Message = message, Success = false });

    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(new ApiResponse { Message = message, Success = false });
    
}
