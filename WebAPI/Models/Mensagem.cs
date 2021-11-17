using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Mensagem
    {
        public int id { get; set; }
        public int Aluno { get; set; }
        public int Professor { get; set; }
        public string Texto { get; set; }
        public string DataHora { get; set; }
    }
}
