using Domen.Entiteti;
using Domen.Interfejsi;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace BazaPodataka
{
    public class ProdajaParfemaDBContext : DbContext, IProdajaParfemaDBContext
    {
        private const string KONEKCIONI_STRING = "Data Source=DESKTOP-J91QQC6\\SQLEXPRESS;Initial Catalog=ParfemiDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DbSet<Parfem> Parfemi => Set<Parfem>();


        public ProdajaParfemaDBContext()
        {
        }

        public ProdajaParfemaDBContext(DbContextOptions<ProdajaParfemaDBContext> opcije) : base(opcije) { }

        protected override void OnConfiguring(DbContextOptionsBuilder izgradjivacOpcija)
        {
            izgradjivacOpcija.UseSqlServer(KONEKCIONI_STRING);
        }

    }
}