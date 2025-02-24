using System.Net;

namespace openpbl.Shared.Exceptions.ExceptionBase;

public abstract class InterviewTestException: SystemException
{
protected InterviewTestException(string message) : base(message) { }
public abstract IList<string> GetErrorMessages();
public abstract HttpStatusCode GetStatusCode();
}