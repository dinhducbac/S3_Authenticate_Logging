using EmployeeManagerment.Models;
using EmployeeManagerment.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagerment.CustomMiddleware
{
    public class LoggingCustomMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IWriteMessage writeMessage)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                writeMessage.WriteMessage("Not authenticated!");
            }
            else
            {
                writeMessage.WriteMessage("Authenticated!");
            }
            await _next(httpContext);
            #region Code cũ
            //if (httpContext.Request.Path == "/api/User/login")
            //{
            //    var memoryStream = new MemoryStream();
            //    httpContext.Response.Body = memoryStream;
            //    await _next(httpContext);
            //    if (httpContext.Response.StatusCode == 200)
            //    {
            //        //writeMessage.WriteMessage("789789789");
            //        try
            //        {
            //            byte[] inputBuffer = new byte[memoryStream.Length];
            //            memoryStream.Read(inputBuffer, 0, inputBuffer.Length);
            //            memoryStream.Seek(0, SeekOrigin.Begin);
            //            var resBodyText = await new StreamReader(memoryStream).ReadToEndAsync();

            //            var resBodyParam = JsonConvert.DeserializeObject<UserModel>(resBodyText);
            //            writeMessage.WriteMessage("Login infomatiom {@data}",resBodyParam);
            //            await httpContext.Response.WriteAsync(resBodyText);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex);
            //            //writeMessage.WriteMessage("Lỗi: " + ex.Message);
            //        }

            //    }
            //}
            //else
            //{
            //    await _next(httpContext);
            //}
            #endregion
        }
        public class ResultViewModel
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public UserModel ResultObject { get; set; }
        }
    }
}
