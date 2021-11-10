﻿using CadAlu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadAlu.Services
{
    class MockEducandos : IEducandoDataStore<Educando>
    {
        readonly List<Educando> educandos;

        public MockEducandos()
        {
            educandos = new List<Educando>()
            {
                new Educando { id = Guid.NewGuid().ToString(), Nome = "Judas Iscariote"},
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
