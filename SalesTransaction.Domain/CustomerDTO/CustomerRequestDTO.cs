using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.CustomerDTO
{
    public class CustomerRequestDTO
    {
        public string CustomerName { get; set; } = String.Empty;

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class CustomerResponseDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = String.Empty;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
