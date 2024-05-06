using SalesTransaction.Application.Enums;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Domain.CustomerDTO;
using SalesTransaction.Domain.Models;
using SalesTransaction.Domain.ProductDTO;
using SalesTransaction.Domain.SalesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application
{
    public class CustomDTOMapper
    {
        public static CustomerResponseDTO MapToDTO(Customer customer)
        {
            return new CustomerResponseDTO
            {
                Id = customer.Id,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                CustomerName = customer.CustomerName,
            };
        }

        public static Customer MapFromDTO(CustomerRequestDTO customer)
        {
            return new Customer
            {
                CustomerName = customer.CustomerName,
            };
        }


        public static ProductResponseDTO MapToDTOProduct(Product product)
        {
            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price
            };
        }

        public static Product MapFromDTOProduct(ProductRequestDTO product)
        {
            return new Product
            {
                Name = product.Name,
                Quantity = product.Quantity,

                Price = product.Price,
            };
        }

        public static SalesResponseDTO MapToDTOSale(SaleTransaction sale)
        {
            return new SalesResponseDTO
            {
                Total = sale.Amount,
            };
        }

        public static SaleTransaction MapFromDTOSale(SaleRequestDTO sale)
        {
            return new SaleTransaction
            {
                ProductId = sale.ProductId,

                CustomerId = sale.CustomerId,
                Quantity = sale.Quantity,

            };
        }





    }
}
