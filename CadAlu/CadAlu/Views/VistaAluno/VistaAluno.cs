using CadAlu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CadAlu.Views.VistaAluno
{
    internal class VistaAluno : ContentPage
    {
        public Aluno Aluno { get; }
        Thickness margin = new Thickness(10);

        public VistaAluno(Aluno aluno)
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
                    AdicionarFoto(),
                    AdicionarDados(),
                    AdicionarBotoes()
                }
            };
            this.Content = allPage;
        }

        private View AdicionarFoto()
        {
            Grid grdFoto = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition{},
                }
            };

            grdFoto.Children.Add(new Image { Source = Preferences.Get("Aluno" + Aluno.Id, String.Empty), WidthRequest = 200, HeightRequest = 200 }, 0, 0);
            return grdFoto;
        }

        private View AdicionarDados()
        {
            Label lblNome = new Label { Text = "Nome: " + Aluno.Nome, HorizontalTextAlignment = TextAlignment.Start };
            Label lblEscola = new Label { Text = "Escola: " + Aluno.Turma.Escola.Nome, HorizontalTextAlignment = TextAlignment.Start };
            Label lblPai = new Label { Text = "Encarregado de educação: "+Aluno.Pai1.Nome, HorizontalTextAlignment = TextAlignment.Start };
            Label lblTelefonePai = new Label { Text = "Contacto: "+Aluno.Pai1.Telefone, HorizontalTextAlignment = TextAlignment.Start };

            Grid infoAluno = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition{},
                    new RowDefinition{},
                    new RowDefinition{},
                    new RowDefinition{}
                }
            };

            infoAluno.Children.Add(lblNome, 0, 1);
            infoAluno.Children.Add(lblEscola, 0, 2);
            infoAluno.Children.Add(lblPai, 0, 3);
            infoAluno.Children.Add(lblTelefonePai, 0, 4);

            StackLayout meio = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand

            };
            meio.Children.Add(infoAluno);

            return meio;
        }

        private View AdicionarBotoes()
        {
            Button btnVoltar = new Button
            {
                Text = "Voltar",
                WidthRequest = 150
            };

            btnVoltar.Clicked += BtnVoltar_Clicked;

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
                        },
                         HorizontalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
            return bottom;
        }
        private void BtnVoltar_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
