using Microsoft.Extensions.Logging;
using SalesTransaction.Application;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Domain.Models;
using SalesTransaction.Domain.SalesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Infrastructure.Repositories
{
    public class SalesTransactionRepository : ISalesTransactionRepository
    {
        private readonly ILogger<SalesTransactionRepository> _logger;

        private IDataAccess _dataAccess;

        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;


        public SalesTransactionRepository(ILogger<SalesTransactionRepository> logger, IDataAccess dataAccess, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _dataAccess = dataAccess;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public async Task<SalesResponseDTO> CreateSales(SaleRequestDTO model)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();

                var productDetail = await _productRepository.GetProductById(model.ProductId);
                var amount = productDetail.Price * model.Quantity;
                parameters["CustomerId"] = model.CustomerId;
                parameters["ProductId"] = model.ProductId;
                parameters["Quantity"] = model.Quantity;
                parameters["Amount"] = amount ;
                parameters["HasInvoice"] = model.HasInvoice;


                parameters["CreatedDate"] = DateTime.Now;




                var res = await _dataAccess.ExecuteProcedureSingleOrDefaultAsync<SaleTransaction>(StoredProcedureName.CreateSaleTransaction, parameters);
                var dto = CustomDTOMapper.MapToDTOSale(res.Entity);


                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding creating sales in repo: {0}", ex.Message);
                throw;
            }
        }

        public async Task<List<SalesResponseDTO>> GetSales()
        {
            List<SalesResponseDTO> result = new List<SalesResponseDTO>();
            try
            {





                var sales = await _dataAccess.ExecuteProcedureQueryAsync<SalesResponseDTO>(StoredProcedureName.GetSales);
                 result = sales.EntityList;
                //foreach (var sale in sales.EntityList)
                //{
                //    int customerId = sale.CustomerId;
                //    int productId = sale.ProductId;
                //    string productName = "";

                //    string customerName = "";



                //    var customer = await _customerRepository.GetCustomerById(customerId);
                //    if (customer != null)
                //    {
                //         customerName = customer.CustomerName; 
                //    }

                //    var product = await _productRepository.GetProductById(productId);
                //    if (product != null)
                //    {
                //         productName = product.Name;
                //    }

                //    SalesResponseDTO response = new SalesResponseDTO()
                //    {
                //        Id = sale.Id,
                //        ProductId = sale.ProductId,
                //        CustomerId = sale.CustomerId,
                //        CustomerName = customerName,
                //        ProductName = productName,
                //        Quantity = sale.Quantity,
                //        Total = sale.Amount,
                //        CreatedDate = sale.CreatedDate,
                //        HasInvoice = sale.HasInvoice,
                //        Rate = product.Price


                //    };

                //    res.Add(response);

                //}

                return result;
                //var dtos = sales.EntityList.Select(sale => CustomDTOMapper.MapToDTO(sale)).ToList();
                //return dtos;
            ;


            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding creating sales in repo: {0}", ex.Message);
                return result;

                throw;
            }
        }
    }
}
