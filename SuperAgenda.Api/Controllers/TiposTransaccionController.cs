using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposTransaccionController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public TiposTransaccionController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoTransaccion>>> GetAll() => await _context.TiposTransaccion.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TipoTransaccion>> Get(int id)
    {
        var item = await _context.TiposTransaccion.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<TipoTransaccion>> Post(TipoTransaccion item)
    {
        _context.TiposTransaccion.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.TiposTransaccion.FindAsync(id);
        if (item is null) return NotFound();
        _context.TiposTransaccion.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
