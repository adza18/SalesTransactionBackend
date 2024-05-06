using Microsoft.Extensions.Logging;
using SalesTransaction.Application;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Domain.Models;
using SalesTransaction.Domain.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;

        private IDataAccess _dataAccess;
        public ProductRepository(ILogger<ProductRepository> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        public async Task<ProductResponseDTO> AddProduct(ProductRequestDTO product)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Name"] = product.Name;
                parameters["Quantity"] = product.Quantity;
                parameters["CreatedDate"] = DateTime.Now;

                parameters["Price"] = product.Price;


                var res = await _dataAccess.ExecuteProcedureSingleOrDefaultAsync<Product>(StoredProcedureName.InsertProduct, parameters);
                var dto = CustomDTOMapper.MapToDTOProduct(res.Entity);


                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding customer in repo: {0}", ex.Message);
                throw ;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Id"] = id;
                var res = await _dataAccess.ExecuteProcedureNonQueryAsync<Product>(StoredProcedureName.DeleteProduct, parameters);
                if (res.IsSuccess)
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

        public async Task<List<ProductResponseDTO>> GetAllProducts()
        {
            var products = await _dataAccess.ExecuteProcedureQueryAsync<Product>(StoredProcedureName.GetAllProduct);

            var dtos = products.EntityList.Select(product => CustomDTOMapper.MapToDTOProduct(product)).ToList();
            return dtos;
        }

        public async Task<ProductResponseDTO> GetProductById(int id)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Id"] = id;
                var res = await _dataAccess.ExecuteProcedureSingleOrDefaultAsync<Product>(StoredProcedureName.GetProductById, parameters);
                var dto = CustomDTOMapper.MapToDTOProduct(res.Entity);
                return dto;

            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retireving customer in repo: {0}", ex.Message);
                throw;

            }
        }

        public async Task<ProductResponseDTO> UpdateProduct(int id, ProductRequestDTO product)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Id"] = id;
                parameters["Name"] = product.Name;
                parameters["Quantity"] = product.Quantity;
                parameters["ModifiedDate"] = DateTime.Now;

                parameters["Price"] = product.Price;


                var res = await _dataAccess.ExecuteProcedureSingleOrDefaultAsync<Product>(StoredProcedureName.UpdateProduct, parameters);

                var dto = CustomDTOMapper.MapToDTOProduct(res.Entity);

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
