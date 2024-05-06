using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.SalesDTO
{
    public class SalesResponseDTO
    {
        public decimal Total { get; set; }
        public int Id { get; set; }
        public decimal Rate { get; set; }

        public int CustomerId  { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool HasInvoice { get; set; }


        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedDate { get; set; }







    }
}
