using CadAlu.ViewModels;
using MySqlConnector;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CadAlu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenPage : ContentPage
    {
        public SplashScreenPage()
        {
            Preferences.Set("appEmail", string.Empty);
            InitializeComponent();
            this.BindingContext = new SplashScreenPageViewModel();
            _ = LigarBDAsync();
        }

        private async Task LigarBDAsync()
        {
            try
            {
                var c1 = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
                c1.Open();
                c1.Close();
                CheckLogin();

            }
            catch (Exception)
            {

                await App.Current.MainPage.DisplayAlert("Info", "Falha de ligação!", "OK");
            }
        }

        private void CheckLogin()
        {
            var appEmail = Preferences.Get("appEmail", string.Empty);

            //O utilizador ainda não fez login pela primeira vez
            if (string.IsNullOrWhiteSpace(appEmail))
            {
                LoginInicial();
            }
            else
            {
                FingerprintAuth();
            }
        }

        private async void LoginInicial()
        {
            IsVisible = false;
            //Shell.Current.GoToAsync(nameof(OneTimeLogin));
            await Navigation.PushAsync(new OneTimeLogin());
        }
        private async void FingerprintAuth()
        {
            var request = new AuthenticationRequestConfiguration("CadAlu", "Por favor, fazer a autenticação para aceder à plataforma.");
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            if (result.Authenticated)
            {
                //await App.Current.MainPage.DisplayAlert("Info", "Olá!", "OK");
                await Navigation.PopAsync();
                Application.Current.MainPage = new MainPage();
                //await Navigation.PushAsync((new AboutPage()));
            }
            else
            {
                // not allowed to do secret stuff :(
            }
        }
    }
}