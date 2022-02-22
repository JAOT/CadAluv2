using CadAlu.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CadAlu.Views.VistaMensagens
{
    internal class VistaMensagem : ContentPage
    {
        Mensagem Mensagem { get; set; }
        public int IdAluno { get; private set; }

        Thickness LabelDefault = new Thickness(10);

        Editor assunto = new Editor();
        Editor mensagem = new Editor();
        Thickness margin = new Thickness(10);

        public VistaMensagem(Mensagem m, int idAluno)
        {
            BackgroundColor = Color.LightGray;

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
                    //AdicionarRemetente(),
                    AdicionarCaixaDeTexto(),
                    AdicionarDocumento(),
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
                BackgroundColor = Color.White,
                Margin = margin,
                Children = {
                                new Label
                                {
                                    Text = "Mensagem",
                                    FontAttributes = FontAttributes.Bold,
                                    FontSize = 24,
                                    BackgroundColor = Color.LightGray,
                                },
                                new ScrollView
                                {
                                    BackgroundColor = Color.White,
                                    Orientation = ScrollOrientation.Vertical,
                                    Content = new Label
                                    {
                                        Text = Mensagem.Texto,
                                        FontAttributes = FontAttributes.None,
                                        FontSize = 16,
                                    }
                                }
                           }
            };
            return texto;
        }
        private StackLayout AdicionarDocumento()
        {
            Label lblDocumento = new Label
            {
                Text = Mensagem.Documento,
                FontAttributes = FontAttributes.None,
                FontSize = 12,
                BackgroundColor = Color.White
            };
            //Adicionar evento de toque para abrir o documento anexado, caso exista
            if (Mensagem.Documento!="")
            {
                TapGestureRecognizer tapLblDoc = new TapGestureRecognizer();
                tapLblDoc.Tapped += DocumentoTapped;
                lblDocumento.GestureRecognizers.Add(tapLblDoc);
            }

            StackLayout documento = new StackLayout
            {
                VerticalOptions = LayoutOptions.End,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.LightGray,
                Margin = margin,
                Children = {
                                new Label
                                {
                                    Text = "Documento anexo"
                                },
                                lblDocumento
                           }
            };
            return documento;
        }

        private async void DocumentoTapped(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync(Mensagem.Documento, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        private StackLayout AdicionarRemetente()
        {


            StackLayout remetente = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Children = {

                           }
            };
            return remetente;
        }

        private StackLayout AdicionarAssunto()
        {
            string textoInicial = "Enviada ";
            //caso tenha sido enviada por um professor, escrever o nome
            if (Mensagem.Pai.Id == 0)
            {
                textoInicial = "Enviado por " + Mensagem.Professor.Nome;
            }

            StackLayout assunto = new StackLayout
            {
                Margin = margin,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.LightGray,
                Children = {
                                new Label
                                {
                                    Text = "Assunto: ",
                                    FontAttributes = FontAttributes.Bold,
                                    FontSize = 24,
                                },
                                new Label
                                {
                                    Text = Mensagem.Tema,
                                    FontAttributes = FontAttributes.Bold,
                                    FontSize = 18,
                                },
                                new Label
                                {
                                    Text = textoInicial + " em " + Mensagem.DataHora.ToShortDateString() + " - " + Mensagem.DataHora.ToShortTimeString(),
                                    FontAttributes = FontAttributes.None,
                                    FontSize = 12,
                                    HorizontalTextAlignment = TextAlignment.End,
                                    VerticalTextAlignment = TextAlignment.End
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
                    var connection = new MySqlConnection("Server=10.0.2.2;Database=cadalu;Uid=android;");
                    connection.Open();
                    var date = DateTime.Now.ToString();
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO MENSAGENS (aluno, tema, texto, professor, documento, pai, lida) VALUES ('" + IdAluno + "', '" + rTema + "', '" + resposta + "', '1', '', '1', '1')";
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
