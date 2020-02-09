using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<CPoroh> CPorohs { get; set; }
        public DbSet<CZarad> CZarads { get; set; }
    }
}
