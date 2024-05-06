using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SalesTransaction.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using SalesTransaction.Domain.DataAccess;

namespace SalesTransaction.Infrastructure.Repositories
{
    public class DataAccess : IDataAccess
    {
        private readonly ILogger<DataAccess> _logger;
        private readonly string _connectingString;

        public DataAccess(IConfiguration config, ILogger<DataAccess> logger)
        {
            _logger = logger;
            _connectingString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<DataAccessResultMultiple<T>> ExecuteProcedureQueryAsync<T>(string sp, Dictionary<string, object>? parameters = null)
        {
            DataAccessResultMultiple<T> result = new DataAccessResultMultiple<T>();
            try
            {
                using (var connection = new SqlConnection(_connectingString))
                {
                    IEnumerable<T> rows;
                    await connection.OpenAsync();
                    if (parameters == null)
                    {
                        rows = await connection.QueryAsync<T>(sp, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        rows = await connection.QueryAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure);
                    }
                    result.IsSuccess = true;
                    result.EntityList = rows.Cast<T>().ToList();
                    result.Message = "Rows fetched successfully";
                 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing sp.");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<DataAccessResult> ExecuteProcedureNonQueryAsync<T>(string sp, Dictionary<string, object>? parameters = null)
        {
            DataAccessResult result = new DataAccessResult();

            try
            {
                using (var connection = new SqlConnection(_connectingString))
                {
                    await connection.OpenAsync();

                    int rowsAffected;

                    if (parameters == null)
                    {
                        using (var command = new SqlCommand(sp, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            rowsAffected = await command.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        using (var command = new SqlCommand(sp, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                            rowsAffected = await command.ExecuteNonQueryAsync();
                        }
                    }
                    if (rowsAffected > 0)
                    {
                        result.IsSuccess = true;
                        result.Message = $"{rowsAffected} rows affected.";
                    }
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing stored procedure.");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }


        public async Task<DataAccessResultSingle<T>> ExecuteProcedureSingleOrDefaultAsync<T>(string sp, Dictionary<string, object>? parameters = null)
        {
            DataAccessResultSingle<T> result = new DataAccessResultSingle<T>();
            try
            {
                    using (var connection = new SqlConnection(_connectingString))
                    {
                    T? row;

                    await connection.OpenAsync();
                    if (parameters == null)
                    {
                        row = await connection.QuerySingleOrDefaultAsync<T>(sp, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        row = await connection.QuerySingleOrDefaultAsync<T>(sp, parameters,commandType: CommandType.StoredProcedure);

                    }

                    if (row is not null)
                    {
                        result.IsSuccess = true;
                        result.Entity = row;
                        result.Message = "Row fetched successfully";
                        return result;
                    }
                    throw new Exception();
                    

                    }
            }
                catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing sp.");
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

   
    }

}
