using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public string status  { get; set; }
    }
}
