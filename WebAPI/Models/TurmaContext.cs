using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class TurmaContext : DbContext
    {
        public TurmaContext(DbContextOptions<TurmaContext> options) : base(options)
        {

        }
        public DbSet<Turma> Turmas { get; set; }
    }
}
