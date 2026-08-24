using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposDeMonedaController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public TiposDeMonedaController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoDeMoneda>>> GetAll() => await _context.TiposDeMoneda.ToListAsync();

    [HttpGet("{tipo}")]
    public async Task<ActionResult<TipoDeMoneda>> Get(string tipo)
    {
        var item = await _context.TiposDeMoneda.FindAsync(tipo);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<TipoDeMoneda>> Post(TipoDeMoneda item)
    {
        _context.TiposDeMoneda.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { tipo = item.Tipo }, item);
    }

    [HttpPut("{tipo}")]
    public async Task<IActionResult> Put(string tipo, TipoDeMoneda item)
    {
        if (tipo != item.Tipo) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.TiposDeMoneda.AnyAsync(e => e.Tipo == tipo)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{tipo}")]
    public async Task<IActionResult> Delete(string tipo)
    {
        var item = await _context.TiposDeMoneda.FindAsync(tipo);
        if (item is null) return NotFound();
        _context.TiposDeMoneda.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
