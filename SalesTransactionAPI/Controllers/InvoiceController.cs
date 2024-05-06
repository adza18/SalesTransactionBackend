using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTransaction.Application.Interfaces;
using SalesTransaction.Application.Services;
using SalesTransaction.Domain.CustomerDTO;
using SalesTransaction.Domain.DataAccess;
using SalesTransaction.Domain.InvoiceDTO;
using SalesTransaction.Domain.Models;
using System.Net;

namespace SalesTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;

        private readonly IInvoiceRepository _invoiceRepository;

        private readonly IInvoiceGenerator _invoiceGenerator;

        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceGenerator invoiceGenerator, IInvoiceRepository invoiceRepository)
        {
            _logger = logger;
            _invoiceGenerator = invoiceGenerator;
            _invoiceRepository = invoiceRepository;
        }

        [HttpPost("GenerateInvoice")]
        public async Task<IActionResult> GenerateInvoice([FromBody] List<InvoiceRequestDTO> model)
        {
            APIResponseDTO<List<InvoiceResponseDTO>> result = new APIResponseDTO<List<InvoiceResponseDTO>>();
            try
            {
                var res = await _invoiceRepository.GenerateInvoice(model);
                result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Customers fetched successfully";
                result.Entity = res;



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customers.");
                result.IsSuccess = false;
                result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError, result);

            }
            return Ok();
        }


        [HttpPost("GenerateInvoicePdf")]
        public  async Task<IActionResult> GenerateInvoicePdf([FromBody] List<InvoiceRequestDTO> model)
        {
            //APIResponseDTO<List<InvoiceResponseDTO>> result = new APIResponseDTO<List<InvoiceResponseDTO>>();
            try
            {
                //var res = await _invoiceRepository.GenerateInvoice(model);
                //result.HttpStatusCode = System.Net.HttpStatusCode.OK;
                //result.Message = "Customers fetched successfully";
                //result.Entity = res;
                var pdf =  await _invoiceGenerator.GenerateInvoice(model);
                return File(pdf, "application/pdf", "invoice.pdf");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customers.");
                //result.IsSuccess = false;
                //result.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                //result.Message = ex.Message;
                //return StatusCode((int)HttpStatusCode.InternalServerError, result);

            }
            return Ok();
        }
    }
}
