using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComidasHechasController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public ComidasHechasController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComidaHecha>>> GetAll() => await _context.ComidasHechas.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ComidaHecha>> Get(int id)
    {
        var item = await _context.ComidasHechas.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<ComidaHecha>> Post(ComidaHecha item)
    {
        item.IdUsuario = this.CurrentUserId();
        _context.ComidasHechas.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ComidasHechas.FindAsync(id);
        if (item is null) return NotFound();
        _context.ComidasHechas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
