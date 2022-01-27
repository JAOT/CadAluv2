using System;
using System.Collections.Generic;
using System.Text;

namespace CadAlu.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public Escola Escola { get; set; }
        public string Nome { get; set; }

    }
}
