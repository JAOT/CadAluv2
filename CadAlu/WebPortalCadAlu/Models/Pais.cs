using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Models
{
    public partial class Pais
    {
        public Pais()
        {
            AlunosPai1Navigation = new HashSet<Alunos>();
            AlunosPai2Navigation = new HashSet<Alunos>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Alunos> AlunosPai1Navigation { get; set; }
        public virtual ICollection<Alunos> AlunosPai2Navigation { get; set; }
    }
}
