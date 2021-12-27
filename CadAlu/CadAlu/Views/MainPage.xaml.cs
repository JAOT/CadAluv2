using CadAlu.Models;
using CadAlu.Views.Tabs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CadAlu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {

        public MainPage()
        {
            InitializeComponent();
            //obter os filhos
            ProcurarFilhos();
            //this.Children.Add(new Principal() { Title = "Principal" });
        }
        private void ProcurarFilhos()
        {
            var connection = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            connection.Open();

            var id = Preferences.Get("appId", 0f);

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM alunos WHERE pai1 = '" + id + "' || pai2 = '" + id + "'";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Aluno aluno = new Aluno();
                aluno.Id = reader.GetInt32("identidade");
                aluno.Nome = reader.GetString("nome");
                aluno.IdTurma = reader.GetInt32("turma");
                Children.Add(new Principal(aluno) { Title = aluno.Nome});
            }
        }
    }
}