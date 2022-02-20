using CadAlu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CadAlu.Views.VistaEscola
{
    internal class VistaEscola : ContentPage
    {
        public Escola Escola { get; }
        Thickness margin = new Thickness(10);
        private Button btnVoltar;

        public VistaEscola(Escola escola)
        {
            this.Escola = escola;
            StackLayout pagina = new StackLayout
            {
                Margin = margin,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    AdicionarDetalhesEscola(),
                    AdicionarMapa(),
                    AdicionarBotoes()

                }
            };
            this.Content = pagina;
        }

        private View AdicionarBotoes()
        {
            btnVoltar = new Button
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

        private View AdicionarMapa()
        {
            Grid mapa = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { }
                }
            };
            mapa.Children.Add(new WebView { Source = this.Escola.Agrupamento.Mapa, HeightRequest = 600, WidthRequest = 600});

            return mapa;
        }

        private View AdicionarDetalhesEscola()
        {
            Grid infoEscola = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition{},
                    new RowDefinition{},
                    new RowDefinition{}
                }
            };
            infoEscola.Children.Add(new Label { Text = Escola.Agrupamento.Nome , HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold}, 0, 1);
            infoEscola.Children.Add(new Label { Text = Escola.Agrupamento.Morada, HorizontalTextAlignment = TextAlignment.Center }, 0, 2);
            infoEscola.Children.Add(new Label { Text = Escola.Agrupamento.Telefone.ToString(), HorizontalTextAlignment = TextAlignment.Center}, 0, 3);
            infoEscola.Children.Add(new Label { Text = Escola.Nome, HorizontalTextAlignment = TextAlignment.Center, FontAttributes = FontAttributes.Bold }, 0, 4);
            infoEscola.Children.Add(new Label { Text = Escola.Morada, HorizontalTextAlignment = TextAlignment.Center }, 0, 5);
            infoEscola.Children.Add(new Label { Text = Escola.Telefone.ToString(), HorizontalTextAlignment = TextAlignment.Center }, 0, 6);

            return infoEscola;
        }
    }
}
