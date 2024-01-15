using Domen.Entiteti;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen.Interfejsi
{
    public interface IProdajaParfemaDBContext
    {
        public DbSet<Parfem> Parfemi { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
