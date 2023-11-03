using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGNet.TestAssignment.Common.WebApi.Models
{
    public class ErorrResponse<T> : ResponseWrapper<T>
    {
        string? StackTrace { get; set; }

        public ErorrResponse(string message, T data, string[]? errors, string? stackTrace) 
            : base(message, data, errors)
        {
            StackTrace = stackTrace;
        }
    }
}
