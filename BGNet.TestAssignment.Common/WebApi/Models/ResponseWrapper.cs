using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGNet.TestAssignment.Common.WebApi.Models
{
    public class ResponseWrapper<T> where T : class
    {
        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public ResponseWrapper( T? data = null, List<string>? errors = null)
        {
            Data = data;
            Errors = errors;
        }

        public static ResponseWrapper<T> WrapToResponse(T? data = null , List<string>? errors = null)
        {

            if (data == null && errors==null)
            {
                return new ResponseWrapper<T>( errors: new List<string>() { "Server Error" });
            }

            return new ResponseWrapper<T>(data, errors);
        }
    }
}
