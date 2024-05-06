using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.ProductDTO
{
    public class ProductRequestDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
