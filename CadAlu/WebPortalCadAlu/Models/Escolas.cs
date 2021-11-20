using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Models
{
    public partial class Escolas
    {
        public Escolas()
        {
            Professores = new HashSet<Professores>();
            Turmas = new HashSet<Turmas>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public short Agrup { get; set; }

        public virtual Agrupamentos AgrupNavigation { get; set; }
        public virtual ICollection<Professores> Professores { get; set; }
        public virtual ICollection<Turmas> Turmas { get; set; }
    }
}
