using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CadAlu.Models
{
    public class ListaMensagem : TextCell
    {
        public Command<Item> ItemTapped { get; }

        public ListaMensagem()
        {
            ItemTapped = new Command<Item>(OnItemSelected);
        }
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            await App.Current.MainPage.DisplayAlert("Info", "Olá!", "OK");
            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}