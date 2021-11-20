using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Models
{
    public partial class Avaliacoes
    {
        public int Id { get; set; }
        public int Aluno { get; set; }
        public int Avaliador { get; set; }
        public string Aval { get; set; }
        public string Tipo { get; set; }

        public virtual Alunos AlunoNavigation { get; set; }
        public virtual Professores AvaliadorNavigation { get; set; }
    }
}
