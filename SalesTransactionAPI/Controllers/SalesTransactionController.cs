using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Domain.CustomerDTO;
using SalesTransaction.Domain.DataAccess;
using SalesTransaction.Domain.Models;
using SalesTransaction.Domain.SalesDTO;

namespace SalesTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesTransactionController : ControllerBase
    {
        private readonly ILogger<SalesTransactionController> _logger;

        private readonly ISalesTransactionRepository _salesTransaction;
        public SalesTransactionController(ILogger<SalesTransactionController> logger, ISalesTransactionRepository salesTransaction)
        {
            _logger = logger;
            _salesTransaction = salesTransaction;
        }

        [HttpPost("CreateSales")]
        public async Task<IActionResult> CreateSales([FromBody] SaleRequestDTO model)
        {
            APIResponseDTO<SalesResponseDTO> result = new APIResponseDTO<SalesResponseDTO>();
            try
            {
                var res = await _salesTransaction.CreateSales(model);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Sales genereated successfully";
                result.Entity = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating sales.");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("GetSales")]
        public async Task<IActionResult> GetSales()
        {
            APIResponseDTO<List<SalesResponseDTO>> result = new APIResponseDTO<List<SalesResponseDTO>>();
            try
            {
                var res = await _salesTransaction.GetSales();
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Sales genereated successfully";
                result.Entity = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating sales.");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return Ok(result);
        }
    }
}
