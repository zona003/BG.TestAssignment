using System.Threading;
using BGNet.TestAssignment.Common.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BGNet.TestAssignment.Common.WebApi.Handlers
{
    public class OperationCancelledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                var exception = (OperationCanceledException)context.Exception;

                context.ExceptionHandled = true;

                var errors = new List<string>();

                if (context.HttpContext.RequestAborted.IsCancellationRequested)
                {
                    errors.Add("Operation cancelled by user");
                }
                else if (exception.CancellationToken.IsCancellationRequested)
                {
                    errors.Add("Operation cancelled by server");
                }
                else
                {
                    errors.Add("Operation cancelled due to internal server error");
                }

                context.Result = new ObjectResult(ResponseWrapper<object>.WrapToResponce(errors));
            }
        }
    }
}