using System;
using System.Collections.Generic;
using System.Text;

namespace CadAlu.Models
{
    internal class Disciplina
    {
        public int Id { get; set; }
        public Turma Turma { get; set; }
        public  Professor Professor { get; set; }
        public string Nome { get; set; }
    }
}
