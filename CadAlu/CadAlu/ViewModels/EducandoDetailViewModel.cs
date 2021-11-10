using CadAlu.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CadAlu.ViewModels
{
    [QueryProperty(nameof(educandoID), nameof(educandoID))]

    public class EducandoDetailViewModel : BaseViewModel
    {
        private string educandoID;
        private string nome;
        private string escola;
        private string turma;
        private string ano;
        private string idade;
        public string Id { get; set; }

        public string Nome
        {
            get => nome;
            set => SetProperty(ref nome, value);
        }
        public string EducandoId
        {
            get
            {
                return educandoID;
            }
            set
            {
                educandoID = value;
                LoadEducandoId(value);
            }
        }
        public async void LoadEducandoId(string educandoID)
        {
            try
            {
                var educando = await EducandoDataStore.GetEducandoAsync(educandoID);
                Id = educando.id;
                nome = educando.Nome;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
