using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Test.POCOs
{
    public class SLCredentials
    {
        public string CompanyDB { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class BatchNumber
    {
        public required string BatchNumberProperty { get; set; }
        public decimal Quantity { get; set; }
    }
}