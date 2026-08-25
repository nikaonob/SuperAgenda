using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntidadesController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public EntidadesController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Entidad>>> GetAll()
    {
        var userId = this.CurrentUserId();
        return await _context.Entidades.Where(e => e.IdUsuario == null || e.IdUsuario == userId).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Entidad>> Get(int id)
    {
        var userId = this.CurrentUserId();
        var item = await _context.Entidades.FindAsync(id);
        if (item is null || (item.IdUsuario is not null && item.IdUsuario != userId)) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Entidad>> Post(Entidad item)
    {
        item.IdUsuario = this.CurrentUserId();
        _context.Entidades.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Entidad item)
    {
        var userId = this.CurrentUserId();
        if (id != item.Id) return BadRequest();

        var existente = await _context.Entidades.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        if (existente is null) return NotFound();
        if (existente.IdUsuario != userId) return Forbid();

        item.IdUsuario = userId;
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Entidades.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = this.CurrentUserId();
        var item = await _context.Entidades.FindAsync(id);
        if (item is null) return NotFound();
        if (item.IdUsuario != userId) return Forbid();

        _context.Entidades.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
