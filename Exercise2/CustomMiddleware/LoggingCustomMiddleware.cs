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
                        Stream orginalStream = httpContext.Response.Body;
                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                        var resBodyText = await new StreamReader(responseBodyStream).ReadToEndAsync();
                        var resBodyParam = JsonConvert.DeserializeObject<UserViewModel>(resBodyText);
                        writeMessage.WriteMessage($"'{resBodyParam.UserName}' đã đăng nhập");
                        writeMessage.WriteMessage($"Thông tin: Username: {resBodyParam.UserName}, Email: {resBodyParam.Email}, Token: {resBodyParam.Token}");
                        responseBodyStream.Position = 0;
                        await responseBodyStream.CopyToAsync(orginalStream);

                        httpContext.Response.Body = orginalStream;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        writeMessage.WriteMessage("Lỗi: " + ex.Message);
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
