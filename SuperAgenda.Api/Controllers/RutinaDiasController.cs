using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RutinaDiasController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public RutinaDiasController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RutinaDia>>> GetAll() => await _context.RutinaDias.OrderBy(r => r.Orden).ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RutinaDia>> Get(int id)
    {
        var item = await _context.RutinaDias.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<RutinaDia>> Post(RutinaDia item)
    {
        _context.RutinaDias.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, RutinaDia item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.RutinaDias.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.RutinaDias.FindAsync(id);
        if (item is null) return NotFound();
        _context.RutinaDias.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
