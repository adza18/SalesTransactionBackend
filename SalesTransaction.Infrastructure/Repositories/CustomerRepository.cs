using Microsoft.Extensions.Logging;
using SalesTransaction.Application;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Domain.CustomerDTO;
using SalesTransaction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;

        private IDataAccess _dataAccess;

        public CustomerRepository(ILogger<CustomerRepository> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }
        public async Task<CustomerResponseDTO> AddCustomer(CustomerRequestDTO customer)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["CustomerName"] = customer.CustomerName;
                parameters["Email"] = customer.Email;
                parameters["PhoneNumber"] = customer.PhoneNumber;
                parameters["CreatedDate"] = DateTime.Now
                    ;



                var res = await _dataAccess.ExecuteProcedureSingleOrDefaultAsync<Customer>(StoredProcedureName.InsertCustomer, parameters);
                var dto = CustomDTOMapper.MapToDTO(res.Entity);


                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding customer in repo: {0}", ex.Message);
                throw;
            }

        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Id"] = id;
                var res = await _dataAccess.ExecuteProcedureNonQueryAsync<Customer>(StoredProcedureName.DeleteCustomer, parameters);
                if(res.IsSuccess)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while saving customer in repo: {0}", ex.Message);
                throw;

            }
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomer()
        {
            
            var customers = await _dataAccess.ExecuteProcedureQueryAsync<Customer>(StoredProcedureName.GetAllCustomer);

            var dtos = customers.EntityList.Select(customer => CustomDTOMapper.MapToDTO(customer)).ToList();
            return dtos;
        }

        public async Task<CustomerResponseDTO> GetCustomerById(int id)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Id"] = id;
                var res = await _dataAccess.ExecuteProcedureSingleOrDefaultAsync<Customer>(StoredProcedureName.GetCustomerById, parameters);
                var dto = CustomDTOMapper.MapToDTO(res.Entity);
                return dto;

            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retireving customer in repo: {0}", ex.Message);
                throw;

            }
        }

        public async Task<CustomerResponseDTO> UpdateCustomer(int id, CustomerRequestDTO customer)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["CustomerName"] = customer.CustomerName;
                parameters["Email"] = customer.Email;
                parameters["PhoneNumber"] = customer.PhoneNumber;
                parameters["ModifiedDate"] = DateTime.Now;


                parameters["Id"] = id;


                var res = await _dataAccess.ExecuteProcedureSingleOrDefaultAsync<Customer>(StoredProcedureName.UpdateCustomer, parameters);
                var dto = CustomDTOMapper.MapToDTO(res.Entity);

                return dto;
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating customer in repo: {0}", ex.Message);
                throw;

            }
        }

      
    }
}
