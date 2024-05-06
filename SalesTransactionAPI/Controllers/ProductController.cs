using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Domain.ProductDTO;
using SalesTransaction.Domain.DataAccess;

namespace SalesTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IProductRepository _product;

        public ProductController(ILogger<ProductController> logger, IProductRepository product)
        {
            _logger = logger;
            _product = product;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            APIResponseDTO<List<ProductResponseDTO>> result = new APIResponseDTO<List<ProductResponseDTO>>();
            try
            {
                var res = await _product.GetAllProducts();
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Products fetched successfully";
                result.Entity = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Products.");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            APIResponseDTO<ProductResponseDTO> result = new APIResponseDTO<ProductResponseDTO>();

            try
            {
                var res = await _product.GetProductById(id);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Product fetched successfully";
                result.Entity = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching Product with ID: {id}");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }

            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDTO Product)
        {
            APIResponseDTO<ProductResponseDTO> result = new APIResponseDTO<ProductResponseDTO>();

            try
            {
                var res = await _product.AddProduct(Product);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Product added successfully";
                result.Entity = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding Product.");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return Ok(result);

        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDTO Product)
        {
            APIResponseDTO<ProductResponseDTO> result = new APIResponseDTO<ProductResponseDTO>();
            try
            {
                var res = await _product.UpdateProduct(id, Product);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Product updated successfully";
                result.Entity = res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating Product with ID: {id}");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return Ok(result);

        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            APIResponseDTO<ProductResponseDTO> result = new APIResponseDTO<ProductResponseDTO>();

            try
            {
                var res = await _product.DeleteProduct(id);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Product deleted successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting Product with ID: {id}");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return Ok(result);

        }
    }
}
