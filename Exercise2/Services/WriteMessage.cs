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
        //void IWriteMessage.WriteMessage(string message, object ob)
        //{
        //    Logger.LogInformation(message, ob);
        //    //Logger.LogDebug(message);
        //    //Logger.LogWarning(message); 
        //    //Logger.LogError(message);
        //}
        void IWriteMessage.WriteMessage(string message)
        {
            Logger.LogInformation(message);
            //Logger.LogDebug(message);
            //Logger.LogWarning(message); 
            //Logger.LogError(message);
        }

    }
}
