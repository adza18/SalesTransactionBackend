using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.DataAccess
{
    public class DataAccessResultSingle<T>
    {
        public bool IsSuccess { get; set; }
        public T Entity { get; set; }

        public string? Message { get; set; }



    }

    public class DataAccessResultMultiple<T>
    {
        public bool IsSuccess { get; set; }
        public List<T> EntityList { get; set; }

        public string? Message { get; set; }



    }
    public class DataAccessResult
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }



    }
}
