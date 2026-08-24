using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public CuentasController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cuenta>>> GetAll() => await _context.Cuentas.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cuenta>> Get(int id)
    {
        var item = await _context.Cuentas.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Cuenta>> Post(Cuenta item)
    {
        _context.Cuentas.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Cuenta item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Cuentas.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Cuentas.FindAsync(id);
        if (item is null) return NotFound();
        _context.Cuentas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
