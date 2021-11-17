using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class AgrupamentoContext : DbContext
    {
        public AgrupamentoContext(DbContextOptions<AgrupamentoContext> options) : base (options)
        {

        }
        public DbSet<Agrupamento> Agrupamentos { get; set; }
    }
}
