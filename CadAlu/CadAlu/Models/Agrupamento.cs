using System;
using System.Collections.Generic;
using System.Text;

namespace CadAlu.Models
{
    public class Agrupamento
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Morada { get; internal set; }
        public double Telefone { get; internal set; }
        public string Mapa { get; internal set; }
    }
}
