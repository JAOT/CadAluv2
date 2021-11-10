using CadAlu.Models;
using CadAlu.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CadAlu.ViewModels
{
    class PerfilPageViewModel : BaseViewModel
    {
        private Educando _selectedEducando;

        public ObservableCollection<Educando> Educandos { get; }
        public Command LoadEducandosCommand { get; }
        public Command<Educando> EducandoTapped { get; }

        public PerfilPageViewModel()
        {
            Title = "Perfil";
            Educandos = new ObservableCollection<Educando>();
            LoadEducandosCommand = new Command(async () => await ExecuteLoadEducandosCommand());
            

            EducandoTapped = new Command<Educando>(OnEducandoSelected);

        }

        async void OnEducandoSelected(Educando educando)
        {
            if (educando == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EducandoDetailPage)}?{nameof(EducandoDetailViewModel.EducandoId)}={educando.id}");
        }

        async Task ExecuteLoadEducandosCommand()
        {
            IsBusy = true;

            try
            {
                Educandos.Clear();
                var educandos = await EducandoDataStore.GetEducandosAsync(true);

                foreach (Educando educando in educandos)
                {
                    Educandos.Add(educando);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedEducando = null;
        }
        public Educando SelectedEducando
        {
            get => _selectedEducando;
            set
            {
                SetProperty(ref _selectedEducando, value);
                OnEducandoSelected(value);
            }
        }

    }
}
