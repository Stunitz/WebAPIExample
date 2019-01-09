using NLog;
using System.Web.Http.ExceptionHandling;

namespace WebAPIExample.Filters.Global
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        protected static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public override void Log(ExceptionLoggerContext context)
        {
            logger.Info($"Error Message: {context.Exception.Message}\n");
        }
    }
}