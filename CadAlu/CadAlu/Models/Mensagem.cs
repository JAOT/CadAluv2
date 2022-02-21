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
        public Pai Pai { get; internal set; }
        public Mensagem Parente { get; internal set; }
        public long Aluno { get; internal set; }
        public long Lida { get; internal set; }
        public string NomeProfessor { get; private set; }
        public string Documento { get; set; }

        public Mensagem()
        {

        }

    }
}
