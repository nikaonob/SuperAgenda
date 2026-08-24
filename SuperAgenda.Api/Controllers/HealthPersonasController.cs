using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthPersonasController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public HealthPersonasController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HealthPersona>>> GetAll() => await _context.HealthPersonas.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HealthPersona>> Get(int id)
    {
        var item = await _context.HealthPersonas.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<HealthPersona>> Post(HealthPersona item)
    {
        _context.HealthPersonas.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, HealthPersona item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.HealthPersonas.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.HealthPersonas.FindAsync(id);
        if (item is null) return NotFound();
        _context.HealthPersonas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
