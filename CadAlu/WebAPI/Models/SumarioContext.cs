using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class SumarioContext : DbContext
    {
        public SumarioContext(DbContextOptions<SumarioContext> options) : base(options)
        {

        }
        public DbSet<Sumario> Sumarios { get; set; }
    }
}
