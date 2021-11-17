using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class PaiContext : DbContext
    {
        public PaiContext(DbContextOptions<PaiContext> options) : base(options)
        {

        }
        public DbSet<Pai> Pais { get; set; }
    }
}
