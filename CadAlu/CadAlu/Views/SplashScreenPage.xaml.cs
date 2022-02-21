using CadAlu.ViewModels;
using MySqlConnector;
using Plugin.Connectivity;
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
            //abanar para refrescar mensagens
            //identificar sensores de autenticação (fingerprint, câmara, pin, padrão)
            //usar câmara para tirar foto dos alunos
            //voz?

            //Preferences.Set("appEmail", string.Empty);
            InitializeComponent();
            this.BindingContext = new SplashScreenPageViewModel();
            _ = LigarBDAsync();
        }

        private async Task LigarBDAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                bool isLive = await CrossConnectivity.Current.IsRemoteReachable("10.0.2.2");
                if (isLive)
                {
                    var c1 = new MySqlConnection("Server=10.0.2.2;Database=cadalu;Uid=android;");
                    c1.Open();
                    c1.Close();
                    CheckLogin();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Info", "Sem ligação ao servidor!", "OK");
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Info", "Sem ligação de internet!", "OK");
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
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

            await Navigation.PushAsync(new OneTimeLogin());
        }
        private async void FingerprintAuth()
        {
            var biometria = await CrossFingerprint.Current.IsAvailableAsync();
            var b = await CrossFingerprint.Current.GetAuthenticationTypeAsync();
            if (!biometria)
            {
                await DisplayAlert("Aviso", "Não existem métodos de autenticação biométrica configurados.", "Ok");
            }
            var request = new AuthenticationRequestConfiguration("CadAlu", "Por favor, fazer a autenticação para aceder à plataforma.")
            {
                AllowAlternativeAuthentication = true
            };
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            if (result.Authenticated)
            {
                await Navigation.PopAsync();
                Application.Current.MainPage = new MainPage();
            }
        }
    }
}