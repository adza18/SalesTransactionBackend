using SalesTransaction.Domain.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application.Interfaces
{
    public interface IDataAccess
    {

        Task<DataAccessResult> ExecuteProcedureNonQueryAsync<T>(string sp, Dictionary<string, object>? parameters = null);

        Task<DataAccessResultMultiple<T>> ExecuteProcedureQueryAsync<T>(string sp, Dictionary<string, object>? parameters = null);

        Task<DataAccessResultSingle<T>> ExecuteProcedureSingleOrDefaultAsync<T>(string sp, Dictionary<string, object>? parameters = null);






    }

 

}
