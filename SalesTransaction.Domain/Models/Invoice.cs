using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.Models
{
    public class Invoice : BaseModel
    {
        public int Id { get; set; } 
        public int TransactionId { get; set; }

        [ForeignKey("TransactionId")]

        public SaleTransaction SalesTransaction { get; set; }


        public decimal TotalAmount { get; set; }
    }
}
