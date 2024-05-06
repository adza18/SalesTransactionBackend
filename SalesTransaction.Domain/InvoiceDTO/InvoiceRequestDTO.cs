﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Domain.InvoiceDTO
{
    public class InvoiceRequestDTO
    {
        public int CustomerId { get; set; }
        public int InvoiceId { get; set; }
    }
    public class PdfRequest
    {
        public string Model { get; set; }
    }

}
