using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcApp.Models
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext (DbContextOptions<InvoiceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<MvcApp.Models.Invoice> Invoices { get; set; }
    }
}
