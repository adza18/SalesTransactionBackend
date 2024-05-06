using Microsoft.Extensions.Logging;
using SalesTransaction.Application;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Domain.InvoiceDTO;
using SalesTransaction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ILogger<InvoiceRepository> _logger;

        private IDataAccess _dataAccess;
        public InvoiceRepository(ILogger<InvoiceRepository> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }
        public  async Task<List<InvoiceResponseDTO>> GenerateInvoice(List<InvoiceRequestDTO> list)
        {
            try
            {
                var customerIds = string.Join(",", list.Select(x => x.CustomerId));
                var invoiceIds = string.Join(",", list.Select(x => x.InvoiceId));


                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["CustomerIds"] = customerIds;
                parameters["TransactionIds"] = invoiceIds;


                var customers = await _dataAccess.ExecuteProcedureQueryAsync<InvoiceResponseDTO>(StoredProcedureName.GenerateInvoice, parameters);

                return customers.EntityList;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retireving customer in repo: {0}", ex.Message);
                throw;

            }
            ////var dtos = customers.EntityList.Select(customer => CustomDTOMapper.MapToDTO(customer)).ToList();
            //return dtos;
        }
    }
}
