using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Domain.CustomerDTO;
using SalesTransaction.Domain.DataAccess;
using System.Net;

namespace SalesTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        private readonly ICustomerRepository _customer;

        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customer)
        {
            _logger = logger;
            _customer = customer;
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            APIResponseDTO<List<CustomerResponseDTO>> result = new APIResponseDTO<List<CustomerResponseDTO>>();
            try
            {
                var res = await _customer.GetAllCustomer();
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Customers fetched successfully";
                result.Entity = res;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customers.");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            }
            return Ok(result);

        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            APIResponseDTO<CustomerResponseDTO> result = new APIResponseDTO<CustomerResponseDTO>();

            try
            {
                var res = await _customer.GetCustomerById(id);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Customer fetched successfully";
                result.Entity = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching customer with ID: {id}");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            }

            return Ok(result);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerRequestDTO customer)
        {
            APIResponseDTO<CustomerResponseDTO> result = new APIResponseDTO<CustomerResponseDTO>();

            try
            {
                var res = await _customer.AddCustomer(customer);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Customer added successfully";
                result.Entity = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding customer.");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            }
            return Ok(result);

        }

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerRequestDTO customer)
        {
            APIResponseDTO<CustomerResponseDTO> result = new APIResponseDTO<CustomerResponseDTO>();
            try
            {
                var res = await _customer.UpdateCustomer(id, customer);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Customer updated successfully";
                result.Entity = res;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating customer with ID: {id}");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            }
            return Ok(result);

        }

        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            APIResponseDTO<CustomerResponseDTO> result = new APIResponseDTO<CustomerResponseDTO>();

            try
            {
                var res = await _customer.DeleteCustomer(id);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Customer deleted successfully";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting customer with ID: {id}");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            }
            return Ok(result);

        }


    }
}
