using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Models
{
    public partial class Disciplinas
    {
        public int Turma { get; set; }
        public int Professor { get; set; }

        public virtual Professores ProfessorNavigation { get; set; }
        public virtual Turmas TurmaNavigation { get; set; }
    }
}
