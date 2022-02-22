using System;
using System.Collections.Generic;
using System.Text;

namespace CadAlu.Models
{
    internal class Avaliacao
    {
        public long Id { get; internal set; }
        public string Aval { get; internal set; }
        public string Tipo { get; internal set; }
        public Professor Avaliador { get; internal set; }
        public Disciplina Disciplina { get; internal set; }
    }
}
