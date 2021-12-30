using System;
using System.Collections.Generic;
using System.Text;

namespace CadAlu.Models
{
    public class Mensagem
    {
        public long Id { get; internal set; }
        public string Tema { get; internal set; }
        public string Texto { get; internal set; }
        public Professor Professor { get; internal set; }
        public DateTime DataHora { get; internal set; }
    }
}
