namespace CadAlu.Models
{
    public class Educando
    {
        public string id { get; set; }
        public string Nome { get; set; }
        private string escola { get; set; }
        private string turma { get; set; }
        private string ano { get; set; }
        private string idade { get; set; }
    }
}