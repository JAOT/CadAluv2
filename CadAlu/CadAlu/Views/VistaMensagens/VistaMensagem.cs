using CadAlu.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CadAlu.Views.VistaMensagens
{
    internal class VistaMensagem : ContentPage
    {
        Mensagem Mensagem { get; set; }
        public int IdAluno { get; private set; }

        Thickness LabelDefault = new Thickness(10);
        StackLayout stkPrincipal = new StackLayout();
        StackLayout stkAssunto = new StackLayout();
        StackLayout stkProfessor = new StackLayout();
        StackLayout stkTexto = new StackLayout();
        StackLayout stkBotoes = new StackLayout();

        Editor assunto = new Editor();
        Editor mensagem = new Editor();
        Thickness margin = new Thickness(10);

        public VistaMensagem(Mensagem m, int idAluno)
        {
            Mensagem = m;
            IdAluno = idAluno;

            StackLayout allPage = new StackLayout
            {
                Margin = margin,
                BackgroundColor = Color.LightGray,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    AdicionarAssunto(),
                    AdicionarRemetente(),
                    AdicionarCaixaDeTexto(),
                    AdicionarBotoes()
                }

            };
            this.Content = allPage;      
        }

        private StackLayout AdicionarBotoes()
        {
            Button btnVoltar = new Button();
            btnVoltar.Text = "Voltar";
            btnVoltar.WidthRequest = 150;
            btnVoltar.Clicked += BtnVoltar_Click;

            Button btnResponder = new Button();
            btnResponder.Text = "Responder";
            btnResponder.WidthRequest = 150;
            btnResponder.Clicked += BtnResponder_Click;

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
                            btnResponder
                        },
                         HorizontalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
            return bottom;
        }

        private StackLayout AdicionarCaixaDeTexto()
        {
            StackLayout texto = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Bisque,
                Margin = margin,
                Children = {
                                new ScrollView
                                {
                                    Orientation = ScrollOrientation.Vertical,
                                    Content = new Label
                                    {
                                        Text = Mensagem.Texto,
                                        FontAttributes = FontAttributes.None,
                                        FontSize = 16,
                                        Margin = LabelDefault
                                    }
                                }

                           }
            };
            return texto;
        }

        private StackLayout AdicionarRemetente()
        {
            StackLayout remetente = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Margin = LabelDefault,
                Children = {
                                new Label
                                {
                                    Text = "Enviado por " + Mensagem.Professor.Nome + " em " + Mensagem.DataHora.ToShortDateString() + " - " + Mensagem.DataHora.ToShortTimeString(),
                                    FontAttributes = FontAttributes.None,
                                    FontSize = 14,
                                    Margin = LabelDefault,
                                    HorizontalTextAlignment = TextAlignment.End,
                                    VerticalTextAlignment = TextAlignment.End
                                }
                           }
            };
            return remetente;
        }

        private StackLayout AdicionarAssunto()
        {
            StackLayout assunto = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Margin = LabelDefault,
                Children = {
                                new Label
                                {
                                    Text = "Assunto: ",
                                    FontAttributes = FontAttributes.Bold,
                                    FontSize = 24,
                                    Margin = LabelDefault
                                },
                                new Label
                                {
                                    Text = Mensagem.Tema,
                                    FontAttributes = FontAttributes.Bold,
                                    FontSize = 18,
                                    Margin = LabelDefault
                                }
                           }
            };
            return assunto;
        }

        private async void BtnResponder_Click(object sender, EventArgs e)
        {
            var rTema = "Re: " + Mensagem.Tema;
            if (!string.IsNullOrEmpty(Mensagem.Tema))
            {
                var resposta = await DisplayPromptAsync(rTema, "Mensagem", "Enviar", "Cancelar");
                if (!string.IsNullOrEmpty(resposta))
                {
                    var connection = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
                    connection.Open();
                    var date = DateTime.Now.ToString();
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO MENSAGENS (aluno, tema, texto, professor) VALUES ('" + IdAluno + "', '" + rTema + "', '" + resposta + "', '1')";
                    try
                    {
                        var reader = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        _ = DisplayAlert("Info", "Erro de ligação.", "OK");
                    }
                }
            }
        }

            private void BtnVoltar_Click(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
