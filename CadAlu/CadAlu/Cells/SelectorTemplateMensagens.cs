using CadAlu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CadAlu.Cells
{
    internal class SelectorTemplateMensagens : DataTemplateSelector
    {
        public DataTemplate Lida { get; set; }
        public DataTemplate NaoLida { get; set; }
        public DataTemplate LidaEsq { get; set; }
        public DataTemplate NaoLidaEsq { get; set; }

        public SelectorTemplateMensagens()
        {
            Lida = new DataTemplate(typeof(CellMensagensLida));
            NaoLida = new DataTemplate(typeof(CellMensagensNaoLida));
            LidaEsq = new DataTemplate(typeof(CellMensagensLidaEsq));
            NaoLidaEsq = new DataTemplate(typeof(CellMensagensNaoLidaEsq));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var m = ((Mensagem)item);
            if(m!=null)
            {
                var lida = ((Mensagem)item).Lida;
                var pai = ((Mensagem)item).Pai;
                //Console.WriteLine(m.Id);
                //Console.WriteLine(pai.Id);
                //tem prof associado  (alinhamento direito)
                //não tem prof associado  (alinhamento esquerdo)
                //foi lida (negrito)
                //não foi lida (normal)
                if (pai.Id == 0) //há professor na mensagem (foi enviada por um professor) alinha à direita
                {
                    if (lida==0)//a mensagem não foi lida
                    {
                        return NaoLida;
                    }
                    else
                    {
                        return Lida;
                    }
                }
                else //foi enviada pelo utilizador 
                {
                    if (lida == 0)//a mensagem não foi lida
                    {
                        return NaoLidaEsq;
                    }
                    else
                    {
                        return LidaEsq;
                    }
                }
            }
            return LidaEsq;

        }
    }
}
