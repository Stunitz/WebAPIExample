using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace WebAPIExample.Filters.Global
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "Oops! Sorry! Something went wrong.\n" +
                "Please contact support@mentoria.com so we can try to find a way to fix it."
            };
        }

        public class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }
            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                    new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(Content),
                        RequestMessage = Request
                    };

                return Task.FromResult(response);
            }
        }
    }
}