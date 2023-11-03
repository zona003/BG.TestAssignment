
namespace BGNet.TestAssignment.Common.WebApi.Models
{
    public class ErrorResponse<T> : ResponseWrapper<T> where T : class
    {
        public string? StackTrace { get; set; }

        public ErrorResponse(T data, IEnumerable<string> errors, string? stackTrace) 
            : base( data, errors)
        {
            StackTrace = stackTrace;
        }
    }
}
