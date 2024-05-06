using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application.Enums
{
   
        public enum ServiceStatusEnum
        {
            Unknown = 0,
            Success = 1,
            NotFound = 2,
            Error = 3,
            PermissionDeniend = 4,
            Unauthorized = 5
        }
}
