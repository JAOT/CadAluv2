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
    //Página de informação mais detalhada do utilizador.
    //de momento, surge apenas o nome do educando com o código do encarregado de educação especificado no código
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
        //método que pesquisa o aluno, apresentando apenas os alunos com o pai1 (encarrwegado de educação com o id = 1)
        async Task ExecuteLoadEducandosCommand()
        {
            IsBusy = true;

            try
            {
                Educandos.Clear();
                var educandos = await EducandoDataStore.GetEducandosAsync(true);

                foreach (Educando educando in educandos)
                {
                    if (educando.pai1 == 1)
                    { 
                        Educandos.Add(educando);
                    }
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
