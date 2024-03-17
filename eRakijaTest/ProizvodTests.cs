using eRakija.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eRakija.Models;

namespace eRakijaTest
{
    
    public class ProizvodiTests
    {
        public ProizvodContext Context { get; set; }
        public ProizvodController ProizvodController { get; set; }

        public static string connectionString = "Server=(localdb)\\eRakija;Database=eRakijaDB";

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            Context = new ProizvodContext(optionsBuilder.Options);

            ProizvodController = new ProizvodController(Context);
        }

        #region PrikaziProizvodByITest

        [Test]
        [TestCase(9)]
        public async Task PrikaziProizvodById_Return_OkResult(int proizvodId)
        {

            //Act  
            var data = await ProizvodController.PrikaziProizvod(proizvodId);
            var result = data as OkObjectResult;

            //Assert
            if (result is OkObjectResult)
            {
                Assert.NotNull(result.Value);
                var pr = result.Value as Proizvod;
                Console.WriteLine($"Nadjen je proizvod: {pr.Naziv}");
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Nije nadjen trazeni proizvod");
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(200)]
        public async Task PrikaziProizvodById_Return_NotFoundResult(int proizvodId)
        {

            //Act  
            var data = await ProizvodController.PrikaziProizvod(proizvodId);
            var result = data as NotFoundObjectResult;

            //Assert
            if (result is NotFoundObjectResult)
            {

                Assert.That(result is NotFoundObjectResult, "I treba da izbaci NotFound u ovom testu");
                Console.WriteLine($"Nije nadjen proizvod sa id-jem: {proizvodId}");
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task PrikaziProizvodById_Return_BadRequest()
        {
            //Arrange  
            int? proizvodId = null;

            //Act  
            var data = await ProizvodController.PrikaziProizvod(proizvodId);
            var result = data as BadRequestObjectResult;

            //Assert
            if (result is BadRequestObjectResult)
            {
                Assert.That(result is BadRequestObjectResult, "I treba da izbaci BadRequest u ovom testu");
                Console.WriteLine($"Nije nadjen proizvod sa id-jem koji je null");
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        #endregion

        #region PrikaziProizvodeByTipITest

        [Test]
        [TestCase(1)]
        public async Task PrikaziProizvodeByTipId_Return_OkResult(int tipId)
        {

            //Act  
            var data = await ProizvodController.PrikaziProizvodePoTipu(tipId);
            var result = data as OkObjectResult;

            //Assert
            if (result is OkObjectResult)
            {
                var proizvodi = result.Value as IEnumerable<Proizvod>;
                Assert.NotNull(proizvodi);
                Assert.True(proizvodi.Count() > 0, "Treba da postoji barem jedan proizvod!");
                Console.WriteLine($"Nadjeni su proizvodi");
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Nisu nadjeni proizvodi");
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(2)] //slucaj kada tipProizvoda nije nadjen
        [TestCase(9)] //slucaj kada je broj proizvoda koji su tog tipa 0 
        public async Task PrikaziProizvodeByTipId_Return_NotFoundResult(int proizvodId)
        {

            //Act  
            var data = await ProizvodController.PrikaziProizvodePoTipu(proizvodId);
            var result = data as NotFoundObjectResult;

            //Assert
            if (result is NotFoundObjectResult)
            {

                Assert.That(result is NotFoundObjectResult, "I treba da izbaci NotFound u ovom testu");
                Console.WriteLine($"Nisu nadjeni proizvodi");
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task PrikaziProizvodeByTipId_Return_BadRequest()
        {
            //Arrange  
            int? tipId = null;

            //Act  
            var data = await ProizvodController.PrikaziProizvodePoTipu(tipId);
            var result = data as BadRequestObjectResult;

            //Assert
            if (result is BadRequestObjectResult)
            {
                Assert.That(result is BadRequestObjectResult, "I treba da izbaci BadRequest u ovom testu");
                Console.WriteLine($"Nisu nadjeni proizvodi za tip sa id-jem koji je null");
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        #endregion

        #region PrikaziSveProizvode

        [Test]
        public async Task PrikaziProizvodeTest()
        {
            var result = await ProizvodController.PrikaziProizvode();

            if (result is OkObjectResult okResult)
            {
                var proizvodi = okResult.Value as IEnumerable<Proizvod>;

                Assert.True(proizvodi.Count() > 0, "Treba da postoji barem jedan proizvod!");
                foreach (var pr in proizvodi)
                {
                    Assert.NotNull(pr.Opis);
                }
                Console.WriteLine("Postoje proizvodi");
            }
            else
            {
                Assert.Fail("PrikaziProizvode funkcija nije vratila OkObjectResult!");
            }
        }

        [Ignore("Ignorisan jer nema nacina da dobijemo NotFound za sad")]
        [Test]
        public async Task PrikaziSveProizvode_Returns_NotFound()
        {
            // Act
            var data = await ProizvodController.PrikaziProizvode();

            // Assert
            Assert.That(data, Is.TypeOf<NotFoundObjectResult>(), "U ovom slucaju je ocekivano da bude NotFound");
            Console.WriteLine("Nije nadjen nijedan proizvod u bazi.");
        }

        [Ignore("Ignorisan jer nema nacina da dobijemo BadRequest za sad")]
        [Test]
        public async Task PrikaziSveProizvode_Returns_BadRequest()
        {

            // Act
            var data = await ProizvodController.PrikaziProizvode();

            // Assert
            Assert.That(data, Is.TypeOf<BadRequestObjectResult>(), "U ovom slucaju je ocekivano da dobijemo BadRequest");
            Console.WriteLine("Bad Request");
        }

        #endregion

        #region UnesiProizvodTest

        [Test]
        [TestCase(1)]
        public async Task UnesiProizvod_Returns_Ok(int tipId)
        {

            var proizvod = new Proizvod
            {
                Naziv = "Proizvod1",
                Cena = 100,
                Slika = "Neka putanja do slike",
                Opis = "Neki opis",
                Kolicina = 1
            };

            var result = await ProizvodController.DodajProizvod(proizvod, tipId);
            if (result is OkObjectResult okResult)
            {
                Assert.NotNull(okResult.Value);
                var pr = okResult.Value as Proizvod;
                Console.WriteLine($"Dodat proizvod: {pr.Naziv}");
                Assert.That(pr.Naziv, Is.EqualTo("Proizvod1"));
                var dbProizovd = await Context.Proizvodi.FindAsync(pr.ID);
                Assert.NotNull(dbProizovd);
            }
            else
            {
                Console.WriteLine($"Neuspešno dodavanje proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(100)]
        public async Task UnesiProizvod_Returns_NotFound(int tipId)
        {

            var proizvod = new Proizvod
            {
                Naziv = "Proizvod1",
                Cena = 100,
                Slika = "Neka putanja do slike",
                Opis = "Neki opis",
                Kolicina = 1
            };

            var result = await ProizvodController.DodajProizvod(proizvod, tipId);
            if (result is NotFoundObjectResult)
            {
                Assert.That(result is NotFoundObjectResult, "I treba da izbaci NotFound u ovom testu jer user ili knjizara ne postoji");
                Console.WriteLine($"Vratio je NotFound");
                Assert.Pass();
            }
            else
            {
                Console.WriteLine($"Neuspešno dodavanje proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(1)]
        public async Task UnesiProizvod_Returns_BadRequest(int tipId)
        {

            var proizvod = new Proizvod
            {
                Naziv = String.Empty,
                Cena = 100,
                Slika = "Neka putanja do slike",
                Opis = "Neki opis",
                Kolicina = 1
            };

            var result = await ProizvodController.DodajProizvod(proizvod, tipId);
            if (result is BadRequestObjectResult)
            {
                Assert.That(result is BadRequestObjectResult, "I treba da izbaci BadRequest u ovom testu jer nije dobar unos");
                Console.WriteLine($"Los unos podataka");
                Assert.Pass();
            }
            else
            {
                Console.WriteLine($"Neuspešno dodavanje proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        #endregion

        #region IzmeniProizvodTest

        [Test]
        [TestCase(37)]
        public async Task IzmeniProizvod_Returns_Ok(int proizvodId)
        {

            var proizvod = new Proizvod
            {
                Naziv = "Proizvod Izmenjen",
                Cena = 10000,
                Slika = "Neka putanja do slike izmenjena",
                Opis = "Neki opis izmenjen",
                Kolicina = 1
            };

            var result = await ProizvodController.IzmeniProizvod(proizvod, proizvodId);
            if (result is OkObjectResult okResult)
            {
                Assert.NotNull(okResult.Value);
                var pr = okResult.Value as Proizvod;
                Console.WriteLine($"Izmenjeni proizvod: {pr.Naziv}");
                var dbProizvod = await Context.Proizvodi.FindAsync(pr.ID);
                Assert.NotNull(dbProizvod);
            }
            else
            {
                Console.WriteLine($"Neuspešno menjanje proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(200)]
        public async Task IzmeniProizvod_Returns_NotFound(int proizvodId)
        {

            var proizvod = new Proizvod
            {
                Naziv = "Proizvod Izmenjen",
                Cena = 10000,
                Slika = "Neka putanja do slike izmenjena",
                Opis = "Neki opis izmenjen",
                Kolicina = 1
            };

            var result = await ProizvodController.IzmeniProizvod(proizvod, proizvodId);
            if (result is NotFoundObjectResult okResult)
            {
                Assert.That(result is NotFoundObjectResult, "I treba da izbaci NotFound");
                Console.WriteLine($"Not found je vracen");
                Assert.Pass("Vracen je NotFound");
            }
            else
            {
                Console.WriteLine($"Neuspešno menjanje knjige. Rezultat: {result}");
                Assert.Fail("Nije vratio NotFound");
            }
        }

        [Test]
        [TestCase(33)]
        public async Task IzmeniKnjigu_Returns_BadRequest(int proizvodId)
        {

            var proizvod = new Proizvod
            {
                Naziv = String.Empty,
                Cena = 10000,
                Slika = "Neka putanja do slike izmenjena",
                Opis = "Neki opis izmenjen",
                Kolicina = 1
            };

            var result = await ProizvodController.IzmeniProizvod(proizvod, proizvodId);
            if (result is BadRequestObjectResult)
            {
                Assert.That(result is BadRequestObjectResult, "I treba da izbaci BadRequest");
                Console.WriteLine($"Uneti podaci nisu korektni");
                Assert.Pass("Vracen je BadRequest");
            }
            else
            {
                Console.WriteLine($"Neuspešno menjanje proizvoda. Rezultat: {result}");
                Assert.Fail("Nije vratio BadRequest");
            }
        }
        #endregion

        #region ObrisiProizvodTest

        [Test]
        [TestCase(36)]
        public async Task ObrisiProizvod_Returns_Ok(int proizvodId)
        {

            var result = await ProizvodController.ObrisiProizvod(proizvodId);
            if (result is OkObjectResult okResult)
            {
                Assert.That(result is OkObjectResult, "Izbacen je OK");
                Console.WriteLine($"Proizvod je izbrisana");
                Assert.Pass("Vracen je OK");
            }
            else
            {
                Console.WriteLine($"Neuspešno brisanje proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(200)]
        public async Task ObrisiProizvod_Returns_NotFound(int proizvodId)
        {

            var result = await ProizvodController.ObrisiProizvod(proizvodId);
            if (result is NotFoundObjectResult)
            {
                Assert.That(result is NotFoundObjectResult, "I treba da izbaci NotFound");
                Console.WriteLine($"Not found je vracen");
                Assert.Pass("Vracen je NotFound");
            }
            else
            {
                Console.WriteLine($"Neuspešno brisanje proizvoda. Rezultat: {result}");
                Assert.Fail("Nije vratio NotFound");
            }
        }

        #endregion

        [TearDown]
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}