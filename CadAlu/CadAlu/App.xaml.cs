using CadAlu.Services;
using CadAlu.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CadAlu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new SplashScreenPage());
        }
    }
}