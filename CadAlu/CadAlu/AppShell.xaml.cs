using CadAlu.ViewModels;
using CadAlu.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CadAlu
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
