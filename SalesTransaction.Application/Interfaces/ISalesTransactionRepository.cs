using SalesTransaction.Domain.ProductDTO;
using SalesTransaction.Domain.SalesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application.Interfaces
{
    public interface ISalesTransactionRepository
    {
        Task<SalesResponseDTO> CreateSales(SaleRequestDTO model);
        Task<List<SalesResponseDTO>> GetSales();


    }
}
