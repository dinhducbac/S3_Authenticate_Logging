using Microsoft.Extensions.Logging;

namespace EmployeeManagerment.Services
{
    public class WriteMessage : IWriteMessage
    {
        public readonly ILogger Logger;
        public WriteMessage(ILogger<WriteMessage> logger)
        {
            Logger = logger;
        }
        void IWriteMessage.WriteMessage(string message)
        {
            Logger.LogInformation(message);
        }

    }
}
