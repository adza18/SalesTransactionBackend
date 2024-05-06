using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTransaction.Application
{
    public static class StoredProcedureName
    {
        public const string InsertCustomer = "InsertCustomer";
        public const string UpdateCustomer = "UpdateCustomer";
        public const string GetAllCustomer = "GetAllCustomer";

        public const string GetCustomerById = "GetCustomerById";

        public const string DeleteCustomer = "DeleteCustomer";



        public const string InsertProduct = "InsertProduct";
        public const string UpdateProduct = "UpdateProduct";
        public const string GetAllProduct = "GetAllProduct";

        public const string GetProductById = "GetProductById";

        public const string DeleteProduct = "DeleteProduct";

        public const string CreateSaleTransaction = "InsertSaleTransaction";
        public const string GetSales = "GetSales";

        public const string GenerateInvoice = "ProcessInvoices";



    }
}
