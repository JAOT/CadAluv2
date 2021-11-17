using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class MensagemContext : DbContext
    {
        public MensagemContext(DbContextOptions<MensagemContext> options) : base(options)
        {

        }
        public DbSet<Mensagem> Mensagens { get; set; }
    }
}
