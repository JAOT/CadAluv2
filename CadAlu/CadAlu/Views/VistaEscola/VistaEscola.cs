using CadAlu.Models;
using Plugin.Messaging;
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
            BackgroundColor = Color.LightGray;

            this.Escola = escola;
            StackLayout pagina = new StackLayout
            {
                Margin = margin,
                BackgroundColor = Color.LightGray,
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
                BackgroundColor = Color.LightGray,
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
                Margin = margin,
                BackgroundColor = Color.LightGray,
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
            Label lblAgrupamentoTelefone = new Label { Text = Escola.Agrupamento.Telefone.ToString(), HorizontalTextAlignment = TextAlignment.Start };
            TapGestureRecognizer tapAgrupamentoTelefone = new TapGestureRecognizer();
            tapAgrupamentoTelefone.Tapped += AgrupamentoTapped;
            lblAgrupamentoTelefone.GestureRecognizers.Add(tapAgrupamentoTelefone);

            Label lblEscolaTelefone = new Label { Text = Escola.Telefone.ToString(), HorizontalTextAlignment = TextAlignment.End };
            TapGestureRecognizer tapEscolaTelefone = new TapGestureRecognizer();
            tapEscolaTelefone.Tapped += EscolaTapped;
            lblEscolaTelefone.GestureRecognizers.Add(tapEscolaTelefone);


            Grid infoEscola = new Grid
            {
                BackgroundColor = Color.LightGray,
                Margin = margin,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition{},
                    new RowDefinition{},
                    new RowDefinition{},
                    new RowDefinition{},
                    new RowDefinition{},
                    new RowDefinition{},
                    new RowDefinition{}
                }
            };
            infoEscola.Children.Add(new Label { Text = Escola.Agrupamento.Nome , HorizontalTextAlignment = TextAlignment.Start, FontAttributes = FontAttributes.Bold}, 0, 1);
            infoEscola.Children.Add(new Label { Text = Escola.Agrupamento.Morada, HorizontalTextAlignment = TextAlignment.Start }, 0, 2);
            infoEscola.Children.Add(lblAgrupamentoTelefone, 0, 3);
            infoEscola.Children.Add(new Label { Text = Escola.Nome, HorizontalTextAlignment = TextAlignment.End, FontAttributes = FontAttributes.Bold }, 0, 4);
            infoEscola.Children.Add(new Label { Text = Escola.Morada, HorizontalTextAlignment = TextAlignment.End }, 0, 5);
            infoEscola.Children.Add(lblEscolaTelefone, 0, 6);

            return infoEscola;
        }

        private void AgrupamentoTapped(object sender, EventArgs e)
        {
            var chamada = CrossMessaging.Current.PhoneDialer;
            if (chamada.CanMakePhoneCall)
            {
                chamada.MakePhoneCall(Escola.Agrupamento.Telefone.ToString());
            }
        }

        private void EscolaTapped(object sender, EventArgs e)
        {
            var chamada = CrossMessaging.Current.PhoneDialer;
            if (chamada.CanMakePhoneCall)
            {
                chamada.MakePhoneCall(Escola.Telefone.ToString());
            }
        }
    }
}
