using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace WebAPIExample
{
    public class NotImplementedFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
            {
                Content = new StringContent("<!DOCTYPE html><html><head><title>Exception Filter Example</title></head><body><h1>Exception Filter Example</h1></body></html>")
            };

            context.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        }
    }
}