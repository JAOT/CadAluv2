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
        public VistaMensagem(Mensagem m, int idAluno)
        {
            Mensagem = m;
            IdAluno = idAluno;
                        
            StackLayout assunto = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Margin =LabelDefault,
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
                                    Text = m.Tema,
                                    FontAttributes = FontAttributes.Bold,
                                    FontSize = 18,
                                    Margin = LabelDefault
                                }
                           }
            };


            StackLayout texto = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White,
                Margin = LabelDefault,
                Children = {
                                new ScrollView
                                {
                                    Orientation = ScrollOrientation.Vertical,
                                    Content = new Label
                                    {
                                        Text = m.Texto,
                                        FontAttributes = FontAttributes.None,
                                        FontSize = 16,
                                        Margin = LabelDefault
                                    }
                                }
                                
                           }
            };

            StackLayout remetente = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Margin = LabelDefault,
                Children = {
                                new Label
                                {
                                    Text = "Enviado por " + m.Professor.Nome + " em " + m.DataHora.ToShortDateString() + " - " + m.DataHora.ToShortTimeString(),
                                    FontAttributes = FontAttributes.None,
                                    FontSize = 14,
                                    Margin = LabelDefault,
                                    HorizontalTextAlignment = TextAlignment.End,
                                    VerticalTextAlignment = TextAlignment.End
                                }
                           }
            };

            //Label lblTexto = new Label();
            //lblTexto.Text = m.Texto;
            //lblTexto.FontAttributes = FontAttributes.Bold;
            //lblTexto.BackgroundColor = Color.LightGray;
            //lblTexto.Margin = LabelDefault;

            //Label lblProfessor = new Label();
            //lblProfessor.Text = m.Professor.Nome;
            //lblProfessor.FontAttributes = FontAttributes.Bold;
            //lblProfessor.BackgroundColor = Color.LightGray;
            //lblProfessor.Margin = LabelDefault;

            //Label lblDataHora = new Label();
            //lblDataHora.Text = m.DataHora.ToShortDateString() + " - " + m.DataHora.ToShortTimeString();
            //lblDataHora.FontAttributes = FontAttributes.Bold;
            //lblDataHora.BackgroundColor = Color.LightGray;
            //lblDataHora.Margin = LabelDefault;


            StackLayout middle = new StackLayout { BackgroundColor = Color.Bisque, VerticalOptions = LayoutOptions.CenterAndExpand };


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
                Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.End, Margin = LabelDefault,
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
            
            StackLayout allPage = new StackLayout
            {
                BackgroundColor = Color.LightGray,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    assunto, texto, remetente, middle, bottom
                }
                
            };
            this.Content = allPage;            
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
