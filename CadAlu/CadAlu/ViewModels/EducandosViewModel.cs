using CadAlu.Models;
using CadAlu.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CadAlu.ViewModels
{
    public class EducandosViewModel : BaseViewModel
    {
        private Educando _selectedEducando;

        public ObservableCollection<Educando> Educandos { get; }
        public Command LoadEducandosCommand { get; }
        public Command<Educando> EducandoTapped { get; }

        public EducandosViewModel()
        {
            Title = "Educandos";
            Educandos = new ObservableCollection<Educando>();
            //LoadEducandosCommand = new Command(async () => await ExecuteLoadEducandosCommand());
            var e1 = new Educando { id = Guid.NewGuid().ToString(), Nome = "Judas Iscariote" };
            var e2 = new Educando { id = Guid.NewGuid().ToString(), Nome = "Jesus Cristo" };
            var e3 = new Educando { id = Guid.NewGuid().ToString(), Nome = "Barrabás" };
            Educandos.Add(e1);
            Educandos.Add(e2);
            Educandos.Add(e3);
            //EducandoTapped = new Command<Educando>(OnEducandoSelected);
        }

        async Task ExecuteLoadEducandosCommand()
        {
            IsBusy = true;

            try
            {
                Educandos.Clear();
                var educandos = await EducandoDataStore.GetEducandosAsync(true);
                foreach (var educando in educandos)
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


        async void OnEducandoSelected(Educando educando)
        {
            if (educando == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EducandoDetailPage)}?{nameof(EducandoDetailViewModel.EducandoId)}={educando.id}");
        }
    }
}