using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGNet.TestAssignment.Common.WebApi.Models
{
    public class ResponseWrapper<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }

        public string[]? Errors { get; set; }

        public ResponseWrapper(string message, T data, string[]? errors)
        {
            Message = message;
            Data = data;
            Errors = errors;
        }
    }
}
