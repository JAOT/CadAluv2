using CadAlu.ViewModels;
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
            //Preferences.Set("appEmail", string.Empty);
            InitializeComponent();
            this.BindingContext = new SplashScreenPageViewModel();
            CheckLogin();
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
            var request = new AuthenticationRequestConfiguration("Prove you have fingers!", "Because without it you can't have access");
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