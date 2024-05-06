using SalesTransaction.Domain.CustomerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerResponseDTO> AddCustomer(CustomerRequestDTO customer);
        Task<CustomerResponseDTO> UpdateCustomer(int id,CustomerRequestDTO customer);
        Task<CustomerResponseDTO> GetCustomerById(int id);

        Task<bool> DeleteCustomer(int id);

        Task<List<CustomerResponseDTO>> GetAllCustomer();



    }
}
