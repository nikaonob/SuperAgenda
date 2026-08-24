using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposComidaController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public TiposComidaController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoComida>>> GetAll() => await _context.TiposComida.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TipoComida>> Get(int id)
    {
        var item = await _context.TiposComida.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<TipoComida>> Post(TipoComida item)
    {
        _context.TiposComida.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.TiposComida.FindAsync(id);
        if (item is null) return NotFound();
        _context.TiposComida.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
