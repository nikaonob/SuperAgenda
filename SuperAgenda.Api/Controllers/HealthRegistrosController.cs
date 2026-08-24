using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthRegistrosController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public HealthRegistrosController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HealthRegistro>>> GetAll() => await _context.HealthRegistros.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HealthRegistro>> Get(int id)
    {
        var item = await _context.HealthRegistros.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<HealthRegistro>> Post(HealthRegistro item)
    {
        _context.HealthRegistros.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, HealthRegistro item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.HealthRegistros.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.HealthRegistros.FindAsync(id);
        if (item is null) return NotFound();
        _context.HealthRegistros.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
