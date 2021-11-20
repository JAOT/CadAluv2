using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Models
{
    public partial class Sumario
    {
        public int Id { get; set; }
        public int Professor { get; set; }
        public int Turma { get; set; }
        public string Sumario1 { get; set; }
        public string Data { get; set; }

        public virtual Professores ProfessorNavigation { get; set; }
        public virtual Turmas TurmaNavigation { get; set; }
    }
}
