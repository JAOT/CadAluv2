using CadAlu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CadAlu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EducandoDetailPage : ContentPage
    {
        public EducandoDetailPage()
        {
            InitializeComponent();
            BindingContext = new EducandoDetailViewModel();

        }
    }
}