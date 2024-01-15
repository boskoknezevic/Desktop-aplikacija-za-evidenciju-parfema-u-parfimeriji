using Domen.Entiteti;
using Domen.Interfejsi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen.Funkcionalnosti
{
    public class ParfemService
    {
        private IProdajaParfemaDBContext dbContext;

        public ParfemService(IProdajaParfemaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Parfem>> SviParfemi(string brendFilter = default, string imeFilter = default, string polFilter = default)
        {
            IQueryable<Parfem> parfemi = dbContext.Parfemi;

            if (!string.IsNullOrEmpty(brendFilter))
            {
                parfemi = parfemi.Where(parfem => parfem.Brend.Contains(brendFilter));
            }

            if (imeFilter != default)
            {
                parfemi = parfemi.Where(parfem => parfem.Ime.Equals(imeFilter));
            }

            if (polFilter != default)
            {
                parfemi = parfemi.Where(parfem => parfem.Pol.Equals(polFilter));
            }

            return await parfemi.ToListAsync();
        }


        public async Task DodajParfem(Parfem parfem)
        {
            dbContext.Parfemi.Add(parfem);
            await dbContext.SaveChangesAsync();
        }

        public async Task IzmeniParfem(Guid id, Parfem izmenjeniParfem)
        {
            Parfem? parfemPromena = await dbContext.Parfemi.FindAsync(id);
            if (parfemPromena is null) throw new KeyNotFoundException("Traženi parfem nije pronađen!");

            parfemPromena.Brend = izmenjeniParfem.Brend;
            parfemPromena.Ime = izmenjeniParfem.Ime;
            parfemPromena.Pol = izmenjeniParfem.Pol;

            await dbContext.SaveChangesAsync();
        }

        public async Task Obrisi(Guid id)
        {
            Parfem? parfemBrisanje = await dbContext.Parfemi.FindAsync(id);
            if (parfemBrisanje is null) throw new KeyNotFoundException("Traženi parfem nije proanđen!");
            dbContext.Parfemi.Remove(parfemBrisanje);
            await dbContext.SaveChangesAsync();
        }
    }
}
