using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Avaliacao
    {
        public int id { get; set; }
        public int Aluno { get; set; }
        public int Avaliador { get; set; }
        public string Aval { get; set; }
        public string Tipo { get; set; }
    }
}
