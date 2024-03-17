namespace eRakija.Models
{
    public class ProizvodContext : DbContext
    {
        public DbSet<Korisnik> Korisnici {get; set;}
        public DbSet<Proizvod> Proizvodi {get; set;}
        public DbSet<TipProizvoda> TipoviProizvoda {get; set;}

        public ProizvodContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}