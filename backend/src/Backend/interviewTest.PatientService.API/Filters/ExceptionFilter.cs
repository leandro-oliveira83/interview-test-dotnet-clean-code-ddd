using interviewTest.PatientService.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using openpbl.Shared.Exceptions.ExceptionBase;

namespace interviewTest.PatientService.API.Filters;

public class ExceptionFilter: IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is InterviewTestException myException)
            HandleProjectException(myException, context);
        else
            ThrowUnknowException(context);
    }

    private static void HandleProjectException(InterviewTestException myException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)myException.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(myException.GetErrorMessages()));
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson("An unexpected error occurred."));
    }
}
