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
        public int IdTurma { get; set; }
    }
}
