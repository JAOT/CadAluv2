using CadAlu.Models;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var msgDataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                var temaLabel = new Label { FontAttributes = FontAttributes.Bold };
                var textoLabel = new Label { FontAttributes = FontAttributes.Bold };
                var professorLabel = new Label { FontAttributes = FontAttributes.Bold };
                var dataLabel = new Label { FontAttributes = FontAttributes.Bold };
                temaLabel.SetBinding(Label.TextProperty, "Tema");
                textoLabel.SetBinding(Label.TextProperty, "Texto");
                professorLabel.SetBinding(Label.TextProperty, "Professor");
                dataLabel.SetBinding(Label.TextProperty, "Data");

                grid.Children.Add(temaLabel, 0, 0);
                //grid.Children.Add(textoLabel, 0, 1);
                //grid.Children.Add(professorLabel, 0, 2);
                //grid.Children.Add(dataLabel, 0, 3);


                return new ViewCell { View = grid };
            });


            ListView listView = new ListView();
            listView.ItemsSource = ObterMensagens();
            listView.ItemTemplate = msgDataTemplate;
            stackLayout.Children.Add(listView);

            this.Content = stackLayout;
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
                msg.Data = r1.GetDateTime(5);
                mensagens.Add(msg);
            }
            c1.Close();
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