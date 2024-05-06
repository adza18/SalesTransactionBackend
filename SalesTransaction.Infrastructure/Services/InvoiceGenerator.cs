using iTextSharp.text.pdf;
using iTextSharp.text;
using SalesTransaction.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SalesTransaction.Infrastructure.Repositories;
using SalesTransaction.Domain.InvoiceDTO;
using SalesTransaction.Application.Interfaces;

namespace SalesTransaction.Infrastructure.Services
{
    public class InvoiceGenerator : IInvoiceGenerator
    {
        private readonly ILogger<InvoiceGenerator> _logger;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceGenerator(ILogger<InvoiceGenerator> logger, IInvoiceRepository invoiceRepository)
        {
            _logger = logger;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<byte[]> GenerateInvoice(List<InvoiceRequestDTO> model)
        {
            try
            {
                var result = await _invoiceRepository.GenerateInvoice(model);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Document document = new Document();
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();


                    foreach (var item in result)
                    {
                        Paragraph title = new Paragraph($"Invoice Details - {item.InvoiceId}");
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 10f; 
                        document.Add(title);

                        PdfPTable table = new PdfPTable(2); 

                        // Set table width
                        table.WidthPercentage = 50; 

                        table.DefaultCell.Padding = 5;

                        table.AddCell(new PdfPCell(new Phrase("Field", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase("Value", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10))) { HorizontalAlignment = Element.ALIGN_CENTER });

                        table.AddCell(new PdfPCell(new Phrase("Invoice ID")) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(item.InvoiceId.ToString())) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase("Customer Name")) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(item.CustomerName)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase("Earliest Invoice Date")) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(item.EarliestInvoiceDate.ToString("MM/dd/yyyy"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase("Total Amount")) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase(item.TotalAmount.ToString("0.00"))) { HorizontalAlignment = Element.ALIGN_CENTER });

                        document.Add(table);

                        document.Add(new Paragraph("\n"));
                    }

                    document.Close();
                    pdfWriter.Close();
                    return memoryStream.ToArray();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while generating the invoice: " + ex.Message);
                throw;
            }
        }

    }
}
