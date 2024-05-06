using SalesTransaction.Domain.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.InvoiceDTO
{
    public class InvoiceResponseDTO
    {
        public int InvoiceId { get; set; }
        public decimal TotalAmount { get; set; }


        public DateTime EarliestInvoiceDate { get; set; }

        public int CustomerId { get; set; }


        public string CustomerName { get; set; }

    }
}
