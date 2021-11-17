using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Turma { get; set; }
        public  int Pai1 { get; set; }
        public int? Pai2 { get; set; }
    }
}
