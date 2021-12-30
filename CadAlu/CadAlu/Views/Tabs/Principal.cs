using CadAlu.Models;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CadAlu.Views.Tabs
{
    public class Principal : ContentPage
    {
        public Aluno Aluno { get; set; }
        Turma Turma { get; set; }
        Escola Escola { get; set; }
        internal Agrupamento Agrupamento { get; private set; }

        public Principal(Aluno aluno)
        {
            this.Aluno = aluno;
            ObterDadosAluno();

            StackLayout stackLayout = new StackLayout();
            stackLayout.Margin = new Thickness(20);
            stackLayout.Spacing = 20;
            Label lblNomeAgrupamento = new Label();
            lblNomeAgrupamento.Text = Agrupamento.Nome;
            stackLayout.Children.Add(lblNomeAgrupamento);

            Label lblNomeEscola = new Label();
            lblNomeEscola.Text = Escola.Nome;
            stackLayout.Children.Add(lblNomeEscola);

            Label lblTurma = new Label();
            lblTurma.Text = Turma.Nome;
            stackLayout.Children.Add(lblTurma);

            ListView listView = new ListView();
            listView.ItemTemplate = new DataTemplate(typeof(ListaMensagem));

            listView.ItemTemplate.SetBinding(ListaMensagem.TextProperty, "Tema");
            listView.ItemTemplate.SetBinding(ListaMensagem.DetailProperty, "DataHora");
            listView.ItemTapped += ListView_ItemTappedAsync;
            listView.ItemsSource = ObterMensagens();
                

            stackLayout.Children.Add(listView);

            this.Content = stackLayout;

        }

        async void ListView_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            Mensagem m = (Mensagem)((ListView)sender).SelectedItem;
            var msg = await DisplayAlert(m.Tema, m.Texto + "\n\nProfessor: " + m.Professor.Nome + "\n\n" + "Enviado :"+m.DataHora, "Responder", "OK");
            var rTema = "Re: " + m.Tema;
            if (msg == true)
            {
               var resposta = await DisplayPromptAsync(rTema, "Mensagem", "Enviar", "Cancelar");
                if (!string.IsNullOrEmpty(resposta))
                {
                    var connection = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO MENSAGENS (aluno, tema, texto, professor, datahora) VALUES ('" + Aluno.Id+"', '" + rTema + "', '" + resposta+"', '1', '"+ DateTime.Now+"')";
                    try
                    {
                        var reader = command.ExecuteNonQuery();
                        //_ = DisplayAlert("Info", "Mensagem Enviada!", "OK");
                    }
                    catch (Exception ex)
                    {
                        _ = DisplayAlert("Info", "Erro de ligação.", "OK");
                    }
                }
            }
        }

        private IEnumerable ObterMensagens()
        {
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "SELECT * FROM mensagens WHERE aluno = '" + Aluno.Id + "'";
            var r1 = com1.ExecuteReader();

            List<Mensagem> mensagens = new List<Mensagem>();

            while (r1.Read())
            {
                Mensagem msg = new Mensagem();
                msg.Id = r1.GetInt64(0);
                msg.Tema = r1.GetString(2);
                msg.Texto = r1.GetString(3);
                msg.Professor = GetProfessor(r1.GetInt64(4));
                msg.DataHora = r1.GetDateTime(5);
                mensagens.Add(msg);
            }
            c1.Close();
            mensagens.Sort((m, mm) => mm.DataHora.CompareTo(m.DataHora));
            return mensagens;
        }
        private Professor GetProfessor(long id)
        {
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "SELECT * FROM professores WHERE identidade = '" + id + "'";
            var r1 = com1.ExecuteReader();
            Professor professor = new Professor();
            while (r1.Read())
            {
                professor.Nome = r1.GetString(1);
            }
            c1.Close();
            return professor;
        }

        void ObterDadosAluno()
        {
            var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c1.Open();
            var com1 = c1.CreateCommand();
            com1.CommandText = "SELECT * FROM turmas WHERE identidade = '" + Aluno.IdTurma + "'";
            var r1 = com1.ExecuteReader();

            while (r1.Read())
            {
                Turma = new Turma();
                Turma.Id = r1.GetInt32(0);
                Turma.Nome = r1.GetString(1);
                Turma.Escola = r1.GetInt32(2);
            }
            c1.Close();

            var c2 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c2.Open();
            var com2 = c2.CreateCommand();
            com2.CommandText = "SELECT * FROM escolas WHERE identidade = '" + Turma.Escola + "'";
            var r2 = com2.ExecuteReader();

            while (r2.Read())
            {
                Escola = new Escola();
                Escola.Id = r2.GetInt32(0);
                Escola.Nome = r2.GetString(1);
                Escola.Agrupamento = r2.GetInt32(2);
            }
            c2.Close();

            var c3 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            c3.Open();
            var com3 = c3.CreateCommand();

            com3.CommandText = "SELECT * FROM agrupamentos WHERE identidade = '" + Escola.Id + "'";
            var r3 = com3.ExecuteReader();

            while (r3.Read())
            {
                Agrupamento = new Agrupamento();
                Agrupamento.Id = r3.GetInt32(0);
                Agrupamento.Nome = r3.GetString(1);
            }
            c3.Close();
        }
    }
}