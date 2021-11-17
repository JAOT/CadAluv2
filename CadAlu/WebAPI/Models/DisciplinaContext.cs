using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class DisciplinaContext : DbContext
    {
        public DisciplinaContext(DbContextOptions<DisciplinaContext> options) : base(options)
        {

        }
        public DbSet<Disciplina> Disciplinas { get; set; }
    }
}
