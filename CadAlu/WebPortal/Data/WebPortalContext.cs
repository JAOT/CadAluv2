using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebPortal.Models;

namespace WebPortal.Data
{
    public class WebPortalContext : DbContext
    {
        public WebPortalContext (DbContextOptions<WebPortalContext> options)
            : base(options)
        {
        }

        public DbSet<WebPortal.Models.Professor> Professores { get; set; }

        public DbSet<WebPortal.Models.Agrupamento> Agrupamentos { get; set; }
    }
}
