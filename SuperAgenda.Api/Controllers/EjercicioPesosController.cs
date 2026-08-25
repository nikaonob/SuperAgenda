using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EjercicioPesosController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public EjercicioPesosController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EjercicioPeso>>> GetAll() => await _context.EjercicioPesos.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EjercicioPeso>> Get(int id)
    {
        var item = await _context.EjercicioPesos.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<EjercicioPeso>> Post(EjercicioPeso item)
    {
        _context.EjercicioPesos.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, EjercicioPeso item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.EjercicioPesos.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.EjercicioPesos.FindAsync(id);
        if (item is null) return NotFound();
        _context.EjercicioPesos.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
