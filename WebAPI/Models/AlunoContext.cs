using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class AlunoContext : DbContext
    {
        public AlunoContext(DbContextOptions<AlunoContext> options) : base (options)
        {

        }
        public DbSet<Aluno> Alunos { get; set; }
    }
}
