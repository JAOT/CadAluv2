using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Models
{
    public partial class Turmas
    {
        public Turmas()
        {
            Alunos = new HashSet<Alunos>();
            Disciplinas = new HashSet<Disciplinas>();
            Sumario = new HashSet<Sumario>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int Escola { get; set; }

        public virtual Escolas EscolaNavigation { get; set; }
        public virtual ICollection<Alunos> Alunos { get; set; }
        public virtual ICollection<Disciplinas> Disciplinas { get; set; }
        public virtual ICollection<Sumario> Sumario { get; set; }
    }
}
