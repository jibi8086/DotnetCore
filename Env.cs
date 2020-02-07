using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Models
{
    public class Envelope<T>
    {
        public bool Success { get; set; }
        public string Response { get; set; }
        public T Data { get; set; }
        public string ExceptionMessage { get; set; }
        public string ErrorType { get; set; }
        public Envelope<TDest> Switch<TDest>(TDest data)
        {
            var dest = new Envelope<TDest>();
            dest.Data = data;
            dest.Response = this.Response;
            dest.ExceptionMessage = this.ExceptionMessage;
            dest.Success = this.Success;
            return dest;
        }

    }
}
