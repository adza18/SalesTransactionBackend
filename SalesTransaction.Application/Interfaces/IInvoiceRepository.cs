using SalesTransaction.Domain.CustomerDTO;
using SalesTransaction.Domain.InvoiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<InvoiceResponseDTO>> GenerateInvoice(List<InvoiceRequestDTO> list);

    }
}
