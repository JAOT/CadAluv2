using CadAlu.Models;
using CadAlu.Views;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CadAlu.ViewModels
{
    public class OneTimeLoginViewModel : BaseViewModel
    {
        private string email;
        private string password;
        public Command SubmeterCommand { get; }
        public Command CancelCommand { get; }
        public OneTimeLoginViewModel()
        {
            //SubmeterCommand = new Command(OnSubmeter, ValidateSubmeter);
            SubmeterCommand = new Command(OnSubmeter); 
            CancelCommand = new Command(OnCancel);
            //this.PropertyChanged += (_, __) => SubmeterCommand.ChangeCanExecute();
        }

        private bool ValidateSubmeter()
        {
            return !String.IsNullOrWhiteSpace(email)
                && !String.IsNullOrWhiteSpace(password);
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



        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await App.Current.MainPage.DisplayAlert("Info", "A aplicação será encerrada.", "OK");
        }

        private async void OnSubmeter()
        {
            var connection = new MySqlConnection("Server=192.168.1.219;Database=cadalu;Uid=android;");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM pais WHERE email = '" + email+"'";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var p = reader.GetString("password");
                var id = reader.GetUInt64("identidade");

                if (p == password)
                {
                    Preferences.Set("appEmail", email);
                    Preferences.Set("appId", id);
                    await Application.Current.MainPage.Navigation.PushAsync( new SplashScreenPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Info", "Password errada!", "OK");
                }
            }            
        }
    }
}
