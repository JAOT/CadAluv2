using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Models
{
    public partial class Alunos
    {
        public Alunos()
        {
            Avaliacoes = new HashSet<Avaliacoes>();
            Mensagens = new HashSet<Mensagens>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int Turma { get; set; }
        public int Pai1 { get; set; }
        public int? Pai2 { get; set; }

        public virtual Pais Pai1Navigation { get; set; }
        public virtual Pais Pai2Navigation { get; set; }
        public virtual Turmas TurmaNavigation { get; set; }
        public virtual ICollection<Avaliacoes> Avaliacoes { get; set; }
        public virtual ICollection<Mensagens> Mensagens { get; set; }
    }
}
