using CadAlu.Views;
using MySqlConnector;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CadAlu.ViewModels
{
    //esta será a página de login, ainda a ser implementada
    public class LoginPageViewModel : BaseViewModel
    {
        public string email;
        public string password;
        public Command LoginCommand { get; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }

        private async void FingerprintAuth()
        {
            var request = new AuthenticationRequestConfiguration("Prove you have fingers!", "Because without it you can't have access");
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            if (result.Authenticated)
            {
                await App.Current.MainPage.DisplayAlert("Info", "Olá!", "OK");
            }
            else
            {
                // not allowed to do secret stuff :(
            }
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private async void OnLoginClicked(object obj)
        {
            var deviceId =      Preferences.Get("my_deviceId", string.Empty);
            if (string.IsNullOrWhiteSpace(deviceId))
            {
                deviceId = System.Guid.NewGuid().ToString();
                Preferences.Set("my_deviceId", deviceId);
            }
            await App.Current.MainPage.DisplayAlert("Info", deviceId, "OK");

            MySqlConnection mySqlConnection = new MySqlConnection("server=PrimeIT-Lenovo;uid=pma;Database=cadalu;");
            mySqlConnection.Open();

            var command = mySqlConnection.CreateCommand();

            command.CommandText = "select * from pais where deviceId = '" + deviceId + "'";

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetString("password").Equals(Password))
                    {
                        await App.Current.MainPage.DisplayAlert("Info", "Bem-vindo " + reader.GetString("nome"), "OK");
                        //Debug.WriteLine(reader.GetString("email"));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Info", "Password errada", "OK");
                    }

                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Info", "A conta indicada não existe", "OK");
            }


            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
