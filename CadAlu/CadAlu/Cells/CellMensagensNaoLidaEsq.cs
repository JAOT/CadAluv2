using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CadAlu.Cells
{
    internal class CellMensagensNaoLidaEsq : ViewCell
    {
        public int Alinhamento { get; set; }
        public CellMensagensNaoLidaEsq()
        {
            var grid = new Grid
            {
                BackgroundColor = Color.Beige,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { },
                    new RowDefinition { }
                },
                Padding = new Thickness(5),
            };

            Label lblAssunto = new Label { FontAttributes = FontAttributes.Bold, FontSize = 18, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
            Label lblremetente = new Label { FontAttributes = FontAttributes.None, FontSize = 12, HorizontalOptions = LayoutOptions.End };
            Label lblDataHora = new Label { FontAttributes = FontAttributes.None, FontSize = 12, HorizontalOptions = LayoutOptions.End };

            lblAssunto.SetBinding(Label.TextProperty, "Tema");
            lblremetente.SetBinding(Label.TextProperty, "Professor.Nome");
            lblDataHora.SetBinding(Label.TextProperty, "DataHora");

            //remetente e datahora, orientados verticalmente
            Grid detalhes = new Grid();
            detalhes.BackgroundColor = Color.Transparent;
            //detalhes.Children.Add(lblremetente);
            detalhes.Children.Add(lblDataHora, 1, 0);
            grid.Children.Add(lblAssunto);
            grid.Children.Add(detalhes, 0, 1);

            View = grid;
        }
        protected override void OnTapped()
        {
            base.OnTapped();
        }
    }
}
