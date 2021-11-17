using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class ProfessorContext : DbContext
    {
        public ProfessorContext(DbContextOptions<ProfessorContext> options) : base(options)
        {

        }
        public DbSet<Professor> Professores { get; set; }
    }
}
