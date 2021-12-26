using CadAlu.Models;
using CadAlu.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CadAlu.Views
{
    public partial class OneTimeLogin : ContentPage
    {
        
        public OneTimeLogin()
        {
            InitializeComponent();
            BindingContext = new OneTimeLoginViewModel();
        }

        public async void ReturnToSplashScreen()
        {
            await Navigation.PushModalAsync(new NavigationPage(new SplashScreenPage()));

        }
    }
}