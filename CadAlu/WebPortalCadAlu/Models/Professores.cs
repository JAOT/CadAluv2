using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Models
{
    public partial class Professores
    {
        public Professores()
        {
            Avaliacoes = new HashSet<Avaliacoes>();
            Disciplinas = new HashSet<Disciplinas>();
            Mensagens = new HashSet<Mensagens>();
            Sumario = new HashSet<Sumario>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }
        public string Password { get; set; }
        public int Escola { get; set; }
        public string Disciplina { get; set; }

        public virtual Escolas EscolaNavigation { get; set; }
        public virtual ICollection<Avaliacoes> Avaliacoes { get; set; }
        public virtual ICollection<Disciplinas> Disciplinas { get; set; }
        public virtual ICollection<Mensagens> Mensagens { get; set; }
        public virtual ICollection<Sumario> Sumario { get; set; }
    }
}
