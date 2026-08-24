using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DolarHoyController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public DolarHoyController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DolarHoy>>> GetAll() => await _context.DolarHoy.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DolarHoy>> Get(int id)
    {
        var item = await _context.DolarHoy.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<DolarHoy>> Post(DolarHoy item)
    {
        _context.DolarHoy.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, DolarHoy item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.DolarHoy.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.DolarHoy.FindAsync(id);
        if (item is null) return NotFound();
        _context.DolarHoy.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
