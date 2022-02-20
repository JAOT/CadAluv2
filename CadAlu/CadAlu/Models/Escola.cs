using System;
using System.Collections.Generic;
using System.Text;

namespace CadAlu.Models
{
    public class Escola
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public Agrupamento Agrupamento { get; internal set; }
        public string Morada { get; internal set; }
        public double Telefone { get; internal set; }
    }
}
