using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.Models
{
    public class SaleTransaction : BaseModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public bool HasInvoice { get; set; } = false;


        public int Quantity { get; set; }
        public decimal Amount { get; set; }


        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
