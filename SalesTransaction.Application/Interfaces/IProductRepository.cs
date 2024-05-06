using SalesTransaction.Domain.CustomerDTO;
using SalesTransaction.Domain.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductResponseDTO> AddProduct(ProductRequestDTO product);
        Task<ProductResponseDTO> UpdateProduct(int id, ProductRequestDTO product);
        Task<ProductResponseDTO> GetProductById(int id);

        Task<bool> DeleteProduct(int id);

        Task<List<ProductResponseDTO>> GetAllProducts();
    }
}
