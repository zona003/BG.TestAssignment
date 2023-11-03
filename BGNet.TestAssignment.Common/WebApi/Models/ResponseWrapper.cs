using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGNet.TestAssignment.Common.WebApi.Models
{
    public class ResponseWrapper<T> where T : class
    {
        public string Message { get; set; }
        public T? Data { get; set; }

        public IEnumerable<string>? Errors { get; set; }

        public ResponseWrapper( T? data = null, IEnumerable<string>? errors = null)
        {
            Data = data;
            Errors = errors;
        }

        public static ResponseWrapper<T> WrapToResponce(T? data = null , IEnumerable<string>? errors = null)
        {

            if (data == null && !errors.Any())
            {
                return new ResponseWrapper<T>( errors: new List<string>() { "Server Error" });
            }

            return new ResponseWrapper<T>(data, errors);
        }
    }
}
