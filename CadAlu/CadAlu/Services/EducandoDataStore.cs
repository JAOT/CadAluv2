using CadAlu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadAlu.Services
{
    public class EducandoDataStore : IEducandoDataStore<Educando>
    {
        readonly List<Educando> educandos;

        public EducandoDataStore()
        {
            educandos = new List<Educando>()
            {
                new Educando { id = Guid.NewGuid().ToString(), Nome = "Judas Iscariote", turma=1, pai1=1},
                new Educando { id = Guid.NewGuid().ToString(), Nome = "Jesus Cristo"},
                new Educando { id = Guid.NewGuid().ToString(), Nome = "Barrabás"},
            };
        }


        public async Task<Educando> GetEducandoAsync(string id)
        {
            return await Task.FromResult(educandos.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<Educando>> GetEducandosAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(educandos);
        }

    }
}
