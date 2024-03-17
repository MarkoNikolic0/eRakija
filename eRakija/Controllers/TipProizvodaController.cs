namespace eRakija.Controllers;

[ApiController]
[Route("[controller]")]
public class TipProizvodaController : ControllerBase
{
    public ProizvodContext Context { get; set; }

    public TipProizvodaController(ProizvodContext context)
    {
        Context = context;
    }

    [HttpGet("PrikaziTipoveProizvoda")]
    public async Task<ActionResult> PrikaziTipoveProizvoda()
    {
        try
        {
            return Ok(await Context.TipoviProizvoda.ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("DodajTipoveProizvoda")]
    public async Task<ActionResult> DodajTipoveProizvoda([FromBody] TipProizvoda tp)
    {
        try
        {
            if(tp.Naziv == string.Empty)
            {
                return BadRequest(tp);
            }
            await Context.TipoviProizvoda.AddAsync(tp);
            await Context.SaveChangesAsync();
            return Ok(tp);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("IzmeniTipProizvoda/{tipProizvodaId}")]
    public async Task<ActionResult> IzmeniTipProizvoda([FromBody] TipProizvoda tp, int tipProizvodaId)
    {
        try
        {
            var pom = await Context.TipoviProizvoda.FindAsync(tipProizvodaId);
            if (pom == null)
            {
                return NotFound(pom);
            }
            if (tp.Naziv == string.Empty)
            {
                return BadRequest(tp);
            }
            else
            {
                pom.Naziv = tp.Naziv;
                Context.TipoviProizvoda.Update(pom);
                await Context.SaveChangesAsync();
                return Ok(pom);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("ObrisiTipProizvoda/{tipID}")]
    public async Task<ActionResult> ObrisiTipProizvoda(int tipID)
    {
        try
        {
            var tp = await Context.TipoviProizvoda.FindAsync(tipID);
            if (tp == null)
            {
                return NotFound(tp);
            }
            else
            {
                Context.TipoviProizvoda.Remove(tp);
                await Context.SaveChangesAsync();
                return Ok(tp);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}