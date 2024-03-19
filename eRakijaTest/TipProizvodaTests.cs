using eRakija.Controllers;
using eRakija.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRakijaTest
{
    public class TipProizvodaTests
    {
        public ProizvodContext Context { get; set; }
        public TipProizvodaController TipProizvodaController { get; set; }

        public static string connectionString = "Server=(localdb)\\eRakija;Database=eRakijaDB";

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            Context = new ProizvodContext(optionsBuilder.Options);

            TipProizvodaController = new TipProizvodaController(Context);
        }

        #region PrikaziTipoveProizvodaTests

        [Test]
        public async Task PrikaziSveTipoveProizvodeTest()
        {
            var result = await TipProizvodaController.PrikaziTipoveProizvoda();

            if (result is OkObjectResult okResult)
            {
                var tipoviProizvoda = okResult.Value as IEnumerable<TipProizvoda>;

                Assert.True(tipoviProizvoda.Count() > 0, "Treba da postoji barem jedan tip proizvoda!");
                foreach (var tpr in tipoviProizvoda)
                {
                    Assert.NotNull(tpr.Naziv);
                }
                Console.WriteLine("Postoje tipovi proizvoda");
            }
            else
            {
                Assert.Fail("PrikaziProizvode funkcija nije vratila OkObjectResult!");
            }
        }

        [Ignore("Ignorisan jer nema nacina da dobijemo NotFound za sad")]
        [Test]
        public async Task PrikaziSveTipoveProizvoda_Returns_NotFound()
        {
            // Act
            var data = await TipProizvodaController.PrikaziTipoveProizvoda();

            // Assert
            Assert.That(data, Is.TypeOf<NotFoundObjectResult>(), "U ovom slucaju je ocekivano da bude NotFound");
            Console.WriteLine("Nije nadjen nijedan tip proizvoda u bazi.");
        }

        [Ignore("Ignorisan jer nema nacina da dobijemo BadRequest za sad")]
        [Test]
        public async Task PrikaziSveTipoveProizvoda_Returns_BadRequest()
        {

            // Act
            var data = await TipProizvodaController.PrikaziTipoveProizvoda();

            // Assert
            Assert.That(data, Is.TypeOf<BadRequestObjectResult>(), "U ovom slucaju je ocekivano da dobijemo BadRequest");
            Console.WriteLine("Bad Request");
        }

        #endregion

        #region DodajTipProizvodaTests

        [Test]
        public async Task UnesiTipProizvoda_Returns_Ok()
        {

            var tip = new TipProizvoda
            {
                Naziv = "TipProizvoda1"
            };

            var result = await TipProizvodaController.DodajTipoveProizvoda(tip);
            if (result is OkObjectResult okResult)
            {
                Assert.NotNull(okResult.Value);
                var tipPr = okResult.Value as TipProizvoda;
                Console.WriteLine($"Dodat tip proizvoda: {tipPr.Naziv}");
                Assert.That(tipPr.Naziv, Is.EqualTo("TipProizvoda1"));
                var dbTipoviProizvoda = await Context.TipoviProizvoda.FindAsync(tipPr.Id);
                Assert.NotNull(dbTipoviProizvoda);
            }
            else
            {
                Console.WriteLine($"Neuspešno dodavanje tipa proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        [Ignore("Ignorisan jer nema nacina da dobije notfound jer ne proveravamo da li ima nesto")]
        [Test]
        public async Task UnesiTipProizvoda_Returns_NotFound()
        {

            var tip = new TipProizvoda
            {
                Naziv = "TipProizvoda1"
            };

            var result = await TipProizvodaController.DodajTipoveProizvoda(tip);
            if (result is NotFoundObjectResult)
            {
                Assert.That(result is NotFoundObjectResult, "I treba da izbaci NotFound u ovom testu jer nesto ne postoji");
                Console.WriteLine($"Vratio je NotFound");
                Assert.Pass();
            }
            else
            {
                Console.WriteLine($"Neuspešno dodavanje tipa proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        [Test]
        public async Task UnesiTipProizvoda_Returns_BadRequest()
        {

            var tip = new TipProizvoda
            {
                Naziv = string.Empty
            };

            var result = await TipProizvodaController.DodajTipoveProizvoda(tip);
            if (result is BadRequestObjectResult)
            {
                Assert.That(result is BadRequestObjectResult, "I treba da izbaci BadRequest u ovom testu jer nije dobar unos");
                Console.WriteLine($"Los unos podataka");
                Assert.Pass();
            }
            else
            {
                Console.WriteLine($"Neuspešno dodavanje tipa proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        #endregion

        #region IzmeniTipProizvodaTests

        [Test]
        [TestCase(5)]
        public async Task IzmeniTipProizvoda_Returns_Ok(int tipId)
        {

            var tip = new TipProizvoda
            {
                Naziv = "Izmenjeni naziv"
            };

            var result = await TipProizvodaController.IzmeniTipProizvoda(tip, tipId);
            if (result is OkObjectResult okResult)
            {
                Assert.NotNull(okResult.Value);
                var tp = okResult.Value as TipProizvoda;
                Console.WriteLine($"Izmenjeni tip proizvoda: {tp.Naziv}");
                var dbTipovi = await Context.TipoviProizvoda.FindAsync(tp.Id);
                Assert.NotNull(dbTipovi);
            }
            else
            {
                Console.WriteLine($"Neuspešno menjanje tipa proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(200)]
        public async Task IzmeniTipProizvoda_Returns_NotFound(int tipId)
        {

            var tip = new TipProizvoda
            {
                Naziv = "Izmenjeni naziv"
            };

            var result = await TipProizvodaController.IzmeniTipProizvoda(tip, tipId);
            if (result is NotFoundObjectResult okResult)
            {
                Assert.That(result is NotFoundObjectResult, "I treba da izbaci NotFound");
                Console.WriteLine($"Not found je vracen");
                Assert.Pass("Vracen je NotFound");
            }
            else
            {
                Console.WriteLine($"Neuspešno menjanje tipa proizvoda. Rezultat: {result}");
                Assert.Fail("Nije vratio NotFound");
            }
        }

        [Test]
        [TestCase(1)]
        public async Task IzmeniTipProizvoda_Returns_BadRequest(int tipId)
        {

            var tip = new TipProizvoda
            {
                Naziv = string.Empty
            };

            var result = await TipProizvodaController.IzmeniTipProizvoda(tip, tipId);
            if (result is BadRequestObjectResult)
            {
                Assert.That(result is BadRequestObjectResult, "I treba da izbaci BadRequest");
                Console.WriteLine($"Uneti podaci nisu korektni");
                Assert.Pass("Vracen je BadRequest");
            }
            else
            {
                Console.WriteLine($"Neuspešno menjanje tipa proizvoda. Rezultat: {result}");
                Assert.Fail("Nije vratio BadRequest");
            }
        }

        #endregion

        #region ObrisiTipProizvodaTests

        [Test]
        [TestCase(4)]
        public async Task ObrisiTipProizvoda_Returns_Ok(int tipId)
        {

            var result = await TipProizvodaController.ObrisiTipProizvoda(tipId);
            if (result is OkObjectResult okResult)
            {
                Assert.That(result is OkObjectResult, "Izbacen je OK");
                Console.WriteLine($"Tip proizvoda je izbrisan");
                Assert.Pass("Vracen je OK");
            }
            else
            {
                Console.WriteLine($"Neuspešno brisanje tipa proizvoda. Rezultat: {result}");
                Assert.Fail();
            }
        }

        [Test]
        [TestCase(200)]
        public async Task ObrisiTipProizvoda_Returns_NotFound(int tipId)
        {

            var result = await TipProizvodaController.ObrisiTipProizvoda(tipId);
            if (result is NotFoundObjectResult)
            {
                Assert.That(result is NotFoundObjectResult, "I treba da izbaci NotFound");
                Console.WriteLine($"Not found je vracen");
                Assert.Pass("Vracen je NotFound");
            }
            else
            {
                Console.WriteLine($"Neuspešno brisanje tipa proizvoda. Rezultat: {result}");
                Assert.Fail("Nije vratio NotFound");
            }
        }


        #endregion
    }
}
