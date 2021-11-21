using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.Models
{
    public class Sumario
    {
        public int id { get; set; }
        public int Professor { get; set; }
        public int Turma { get; set; }
        public string Texto { get; set; }
        public string Data { get; set; }
    }
}
