using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedidasController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public MedidasController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medida>>> GetAll() => await _context.Medidas.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Medida>> Get(int id)
    {
        var item = await _context.Medidas.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Medida>> Post(Medida item)
    {
        _context.Medidas.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Medida item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Medidas.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Medidas.FindAsync(id);
        if (item is null) return NotFound();
        _context.Medidas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
