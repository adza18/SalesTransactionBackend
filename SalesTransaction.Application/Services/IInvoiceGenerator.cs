using SalesTransaction.Domain.InvoiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application.Services
{
    public interface IInvoiceGenerator
    {
        Task<byte[]> GenerateInvoice(List<InvoiceRequestDTO> model);
    }
}
