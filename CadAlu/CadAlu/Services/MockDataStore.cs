using CadAlu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadAlu.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "O seu filho tem piolhos", Description="Apanhou-os hoje na escola." , DataHora="06/11/2012 12:34"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "O Casimiro mordeu a grade de ferro", Description="Hoje, logo após ter comido espinafres. Levado ao dentista o gradeamento afectado. Não há danos estreuturais.", DataHora="06/11/2012 12:34"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Publicação dos horários no placard", Description="Foram hoje publicados os horários para o 6º ano.", DataHora="06/11/2012 12:34"},
            };


        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}