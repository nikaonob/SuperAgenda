using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComboComidaDetallesController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public ComboComidaDetallesController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComboComidaDetalle>>> GetAll() => await _context.ComboComidaDetalles.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ComboComidaDetalle>> Get(int id)
    {
        var item = await _context.ComboComidaDetalles.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<ComboComidaDetalle>> Post(ComboComidaDetalle item)
    {
        _context.ComboComidaDetalles.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ComboComidaDetalles.FindAsync(id);
        if (item is null) return NotFound();
        _context.ComboComidaDetalles.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
