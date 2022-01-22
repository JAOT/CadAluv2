using CadAlu.Models;
using CadAlu.Views.VistaPrincipal;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CadAlu.Views.VistaMensagens
{
    public class VistaNovaMensagem : ContentPage
    {
        public Aluno Aluno { get; }
        public int ProfessorID { get; private set; }

        StackLayout stkPrincipal = new StackLayout();
        StackLayout stkAssunto = new StackLayout();
        StackLayout stkProfessor = new StackLayout();
        StackLayout stkTexto = new StackLayout();
        StackLayout stkBotoes = new StackLayout();

        Editor assunto = new Editor();
        Editor mensagem = new Editor();
        Thickness margin = new Thickness(10);
        public VistaNovaMensagem(Aluno aluno)
        {
            Aluno = aluno;
            
            StackLayout allPage = new StackLayout
            {
                Margin = margin,
                BackgroundColor = Color.LightGray,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    AdicionarAssunto(),
                    AdicionarDestinatario(),
                    AdicionarCaixaDeTexto(),
                    AdicionarBotoes()
                }

            };
            this.Content = allPage;
        }

        private View AdicionarAssunto()
        {
            assunto.Placeholder = "Assunto";
            assunto.PlaceholderColor = Color.LightGray;

            StackLayout stkAssunto = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                BackgroundColor= Color.AliceBlue,
                Margin = margin,
                Children = {
                                new Label
                                {
                                    Text = "Assunto: ",
                                    FontAttributes = FontAttributes.Bold,
                                    FontSize = 24,
                                },
                                assunto
                           }
            };
            return stkAssunto;
        }
        private View AdicionarDestinatario()
        {
            //Obter a lista de professores do aluno
            var listaProfessores = ObterListaDeProfessores();
            Picker destinatarios = new Picker();
            foreach (var p in listaProfessores)
            {
                destinatarios.Items.Add(p.nome);
            }

            destinatarios.SelectedIndexChanged += (sender, args) =>
            {
                if (destinatarios.SelectedIndex == -1)
                {
                    this.ProfessorID = 0;
                }
                else
                {
                    foreach (var p in listaProfessores)
                    {
                        if (p.nome.Equals(destinatarios.Items[destinatarios.SelectedIndex]))
                        {
                            this.ProfessorID = p.id;
                        }
                    }
                }
            };


            StackLayout dest = new StackLayout
            {
                BackgroundColor = Color.Bisque,
                Margin = margin,
                Children =
                {
                    new Label
                    {
                        Text = "Destinatário",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                    },
                    destinatarios
                }
            };
            return dest;
        }
        private List<Destinatario> ObterListaDeProfessores()
        {
            List<Destinatario> nomes = new List<Destinatario>();

            var connection = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "select professores.identidade, professores.nome, professores.disciplina, turmas.nome, alunos.nome, alunos.identidade from professores JOIN disciplinas on disciplinas.professor = professores.identidade join turmas on turmas.identidade=disciplinas.turma join alunos on alunos.turma=turmas.identidade and alunos.identidade = " + Aluno.Id;
            var reader = command.ExecuteReader();
            //try
            //{
                while (reader.Read())
                {
                    Destinatario destinatario = new Destinatario
                    {
                        id = reader.GetUInt16(0),
                        nome = reader.GetString(1)
                    };
                    nomes.Add(destinatario);
                }
                connection.Close();
                return nomes;

            //}
            //catch (Exception ex)
            //{
            //    _ = DisplayAlert("Info", "Erro de ligação.", "OK");
            //}

            return null;
        }
        private View AdicionarCaixaDeTexto()
        {
            mensagem.Placeholder = "Nova mensagem";
            mensagem.PlaceholderColor = Color.LightGray;
            mensagem.VerticalOptions = LayoutOptions.FillAndExpand;

            StackLayout middle = new StackLayout
            {
                BackgroundColor = Color.Bisque,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = margin,
                Children =
                {
                    new Label
                    {
                        Text = "Mensagem",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 24,
                    },
                    mensagem
                }
            };
            return middle;
        }

        private StackLayout AdicionarBotoes()
        {
            Button btnVoltar = new Button();
            btnVoltar.Text = "Voltar";
            btnVoltar.WidthRequest = 150;
            btnVoltar.Clicked += BtnVoltar_Click;

            Button btnEnviar = new Button();
            btnEnviar.Text = "Enviar";
            btnEnviar.WidthRequest = 150;
            btnEnviar.Clicked += BtnEnviar_ClickAsync;

            StackLayout bottom = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                Children =
                {
                    new StackLayout
                    {
                        Orientation= StackOrientation.Horizontal,
                        Children =
                        {
                            btnVoltar,
                            btnEnviar
                        },
                         HorizontalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
            return bottom;
        }
        private async void BtnEnviar_ClickAsync(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(assunto.Text) && !string.IsNullOrEmpty(mensagem.Text))
            {
                    var connection = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
                    connection.Open();
                    var date = DateTime.Now.ToString();
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO MENSAGENS (aluno, tema, texto, professor) VALUES ('" + Aluno.Id + "', '" + assunto.Text + "', '" + this.mensagem.Text + "', "+ ProfessorID + ")";
                    try
                    {
                        var reader = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        _ = DisplayAlert("Info", "Erro de ligação.", "OK");
                    }

                _ = DisplayAlert("Info", "Mensagem enviada.", "OK");
                Application.Current.MainPage = new MainPage();

            }
            else
            {
                await DisplayAlert("Aviso", "A mensagem deve ter todos os campos preenchidos", "Ok");
            }
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}