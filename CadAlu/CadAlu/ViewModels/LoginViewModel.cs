using CadAlu.Views;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace CadAlu.ViewModels
{
    //esta será a página de login, ainda a ser implementada
    public class LoginViewModel : BaseViewModel
    {
        public string email;
        public string password;
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
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
            
            await App.Current.MainPage.DisplayAlert("Info", Email, "OK");
            MySqlConnection mySqlConnection = new MySqlConnection("server=192.168.1.219;uid=pma;Database=cadalu;");
            mySqlConnection.Open();

            var command = mySqlConnection.CreateCommand();
            
            command.CommandText = "select * from pais where email = '"+Email+"'";
            
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
