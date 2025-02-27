using Microsoft.Extensions.Logging;
 using Moq;
 
 namespace CommonTestUtilities.Logging;
 
 public class LoggerBuilder<T>
 {
     private readonly Mock<ILogger<T>> _logger = new Mock<ILogger<T>>();

     public ILogger<T> Build() => _logger.Object;

     public Mock<ILogger<T>> GetMock() => _logger;
 }