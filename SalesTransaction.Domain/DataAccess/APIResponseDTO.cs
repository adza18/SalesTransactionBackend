using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.DataAccess
{
    public class APIResponseDTO<T>
    {
        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; }

        public T? Entity { get; set; }


        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
