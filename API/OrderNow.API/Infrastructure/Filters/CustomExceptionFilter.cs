using System.Net;
using System.Net.Http;
using System.Resources;
using System.Web.Http.Filters;
using OrderNow.Common.CustomException;
using ApplicationException = OrderNow.Common.CustomException.ApplicationException;

namespace OrderNow.API.Infrastructure.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;
            if (exception is ValidationException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetResourceMessage(((ApplicationException)exception).ErrorCodeMessageKey));

            }
            else if (exception is NotFoundException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, GetResourceMessage(((ApplicationException)exception).ErrorCodeMessageKey));

            }
            else
            {
                //TODO:Localize the message below
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Sorry Something went wrong");
            }
        }
        private ResourceManager _resourceManager;
        protected string GetResourceMessage(string key)
        {
            if (_resourceManager == null)
            {
                _resourceManager = new ResourceManager("E_Guest.Resources.Resource", typeof(E_Guest.Resources.Resource).Assembly);
            }

            return _resourceManager.GetString(key);
        }
    }
}