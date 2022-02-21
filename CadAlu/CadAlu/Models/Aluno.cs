using System;
using System.Collections.Generic;
using System.Text;

namespace CadAlu.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdPai1 { get; set;}
        public int IdPai2 { get; set; }
        public Turma  Turma { get; set; }

        public Pai Pai1 { get; set; }
        public Pai Pai2 { get; set; }
    }
}
