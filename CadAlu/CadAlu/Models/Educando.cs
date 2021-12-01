namespace CadAlu.Models
{
    public class Educando
    {
        //modelo para o aluno/educando, conde constam os campos da tabela de alunos
        public string id { get; set; }
        public string Nome { get; set; }
        public int turma { get; set; }
        public int pai1 { get; set; }
        public string pai2 { get; set; }
    }
}