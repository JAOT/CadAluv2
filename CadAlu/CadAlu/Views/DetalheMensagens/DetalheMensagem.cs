using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CadAlu.Views.DetalheMensagens
{
    internal class DetalheMensagem: ViewCell
    {
        public string Assunto { get; }
        public string Nome { get; }

        public DetalheMensagem(string assunto, string nome)
        {
            this.Assunto = assunto;
            this.Nome = nome;

            StackLayout corpo = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        Text = Assunto
                    },
                     new Label
                    {
                        Text = Nome,
                    },
                }
            };
        }


    }
}
