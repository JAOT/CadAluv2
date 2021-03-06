using CadAlu.Models;
using CadAlu.Views.VistaPrincipal;
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
            BackgroundColor = Color.LightGray;

            InitializeComponent();
            //obter os filhos
            ProcurarFilhos();
        }
        private void ProcurarFilhos()
        {
            var connection = new MySqlConnection("Server=10.0.2.2;Database=cadalu;Uid=android;");
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
                Children.Add(new VistaPrincipal.VistaPrincipal(aluno) { Title = aluno.Nome});
            }
        }
    }
}