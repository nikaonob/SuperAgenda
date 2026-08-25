using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposMedidaController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public TiposMedidaController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoMedida>>> GetAll() => await _context.TiposMedida.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TipoMedida>> Get(int id)
    {
        var item = await _context.TiposMedida.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<TipoMedida>> Post(TipoMedida item)
    {
        _context.TiposMedida.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.TiposMedida.FindAsync(id);
        if (item is null) return NotFound();
        _context.TiposMedida.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
