using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CadAlu.ViewModels
{
    /// <summary>
    /// Página de início da aplicação. Ainda se mentém o ecrã por defeito, a ser alterado para o ecrã de login ou ecrã de início da aplicação 
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}