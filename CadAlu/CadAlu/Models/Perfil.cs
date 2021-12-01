using System;
using System.Collections.Generic;
using System.Text;

namespace CadAlu.Models
{
    //modelo para o utilizador principal da aplicação
    public class Perfil
    {
        public string id;
        public string Nome = "Zé";
        public string Email;
        public List<Educando> Educandos;
    }
}
