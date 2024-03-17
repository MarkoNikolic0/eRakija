using Microsoft.AspNetCore.Http.HttpResults;

namespace eRakija.Controllers;

[ApiController]
[Route("[controller]")]
public class ProizvodController : ControllerBase
{
    public ProizvodContext Context { get; set; }

    public ProizvodController(ProizvodContext context)
    {
        Context = context;
    }

    [HttpGet("PrikaziProizvod/{id}")]
    public async Task<ActionResult> PrikaziProizvod(int? id)
    {
        try
        {
            if (id == null)
            {
                return BadRequest(id);
            }
            var pr = await Context.Proizvodi.FindAsync(id);
            if (pr == null)
            {
                return NotFound(pr);
            }
            return Ok(pr);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PrikaziProizvode")]
    public async Task<ActionResult> PrikaziProizvode()
    {
        try
        {
            return Ok(await Context.Proizvodi.Include(p => p.tipProizvoda).ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PrikaziProizvodePoTipu/{idTipa}")]
    public async Task<ActionResult> PrikaziProizvodePoTipu(int? idTipa)
    {
        try
        {
            if (idTipa == null)
            {
                return BadRequest(idTipa);
            }
            var tip = await Context.TipoviProizvoda.FindAsync(idTipa);
            if (tip == null)
            {
                return NotFound(tip);
            }
            var pr = await Context.Proizvodi.Where(p => (p.tipProizvoda!.Id == idTipa)).Include(p => p.tipProizvoda).ToListAsync();
            if (pr == null)
            {
                return NotFound(pr);
            }
            return Ok(pr);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajProizvod/{tipID}")]
    public async Task<ActionResult> DodajProizvod([FromBody] Proizvod p, int tipID)
    {
        try
        {
            if (p.Naziv == String.Empty)
            {
                return BadRequest(p);
            }
            var tp = await Context.TipoviProizvoda.FindAsync(tipID);
            if (tp == null)
            {
                return NotFound(tp);
            }
            else
            {
                p.tipProizvoda = tp;
                await Context.Proizvodi.AddAsync(p);
                await Context.SaveChangesAsync();
                return Ok(p);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("IzmeniProizvod/{proizvodId}")]
    public async Task<ActionResult> IzmeniProizvod([FromBody] Proizvod p, int proizvodId)
    {
        try
        {
            var pr = await Context.Proizvodi.FindAsync(proizvodId);
            if (p.Naziv == string.Empty)
            {
                return BadRequest(pr);
            }
            if (pr == null)
            {
                return NotFound(pr);
            }
            else
            {
                pr.Naziv = p.Naziv;
                pr.Opis = p.Opis;
                pr.Slika = p.Slika;
                pr.Cena = p.Cena;
                pr.Kolicina = p.Kolicina;
                Context.Proizvodi.Update(pr);
                await Context.SaveChangesAsync();
                return Ok(pr);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("ObrisiProizvod/{proizvodID}")]
    public async Task<ActionResult> ObrisiProizvod(int proizvodID)
    {
        try
        {
            var s = await Context.Proizvodi.FindAsync(proizvodID);
            if (s == null)
            {
                return NotFound(s);
            }
            else
            {
                Context.Proizvodi.Remove(s);
                await Context.SaveChangesAsync();
                return Ok(s);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
