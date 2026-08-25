using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComidasDiaController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public ComidasDiaController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComidaDia>>> GetAll() => await _context.ComidasDia.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ComidaDia>> Get(int id)
    {
        var item = await _context.ComidasDia.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<ComidaDia>> Post(ComidaDia item)
    {
        item.IdUsuario = this.CurrentUserId();
        _context.ComidasDia.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ComidasDia.FindAsync(id);
        if (item is null) return NotFound();
        _context.ComidasDia.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
