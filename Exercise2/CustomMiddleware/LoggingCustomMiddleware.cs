using EmployeeManagerment.Models;
using EmployeeManagerment.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
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
            if (httpContext.Request.Path == "/api/User/login")
            {
                var responseBodyStream = new MemoryStream();
                httpContext.Response.Body = responseBodyStream;

                await _next(httpContext);
                if (httpContext.Response.StatusCode == 200)
                {
                    //writeMessage.WriteMessage("789789789");
                    try
                    {
                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                        var resBodyText = await new StreamReader(responseBodyStream).ReadToEndAsync();
                        var resBodyParam = JsonConvert.DeserializeObject<ResultViewModel>(resBodyText);
                        writeMessage.WriteMessage($"'{resBodyParam.ResultObject.UserName}' đã đăng nhập");
                        writeMessage.WriteMessage($"Thông tin: Username: {resBodyParam.ResultObject.UserName}, Email: {resBodyParam.ResultObject.Email}, Token: {resBodyParam.ResultObject.Token}");

                    }
                    catch (Exception ex)
                    {

                    }
                    
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
        public class ResultViewModel
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public UserViewModel ResultObject { get; set; }
        }
    }
}
