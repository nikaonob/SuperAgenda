using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CombosComidaController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public CombosComidaController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComboComida>>> GetAll() => await _context.CombosComida.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ComboComida>> Get(int id)
    {
        var item = await _context.CombosComida.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<ComboComida>> Post(ComboComida item)
    {
        item.IdUsuario = this.CurrentUserId();
        _context.CombosComida.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, ComboComida item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.CombosComida.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.CombosComida.FindAsync(id);
        if (item is null) return NotFound();
        _context.CombosComida.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
