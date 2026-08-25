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

    private IQueryable<int> PersonaIdsAccesibles(int userId) =>
        _context.UsuarioHealthPersonas.Where(uhp => uhp.IdUsuario == userId).Select(uhp => uhp.IdHealthPersona);

    private async Task<bool> TieneAcceso(int personaId, int userId) =>
        await _context.UsuarioHealthPersonas.AnyAsync(uhp => uhp.IdUsuario == userId && uhp.IdHealthPersona == personaId);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HealthPersona>>> GetAll()
    {
        var userId = this.CurrentUserId();
        var ids = PersonaIdsAccesibles(userId);
        return await _context.HealthPersonas.Where(p => ids.Contains(p.Id)).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HealthPersona>> Get(int id)
    {
        var userId = this.CurrentUserId();
        if (!await TieneAcceso(id, userId)) return Forbid();

        var item = await _context.HealthPersonas.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<HealthPersona>> Post(HealthPersona item)
    {
        var userId = this.CurrentUserId();
        _context.HealthPersonas.Add(item);
        await _context.SaveChangesAsync();

        _context.UsuarioHealthPersonas.Add(new UsuarioHealthPersona { IdUsuario = userId, IdHealthPersona = item.Id });
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, HealthPersona item)
    {
        var userId = this.CurrentUserId();
        if (!await TieneAcceso(id, userId)) return Forbid();
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
        var userId = this.CurrentUserId();
        if (!await TieneAcceso(id, userId)) return Forbid();

        var item = await _context.HealthPersonas.FindAsync(id);
        if (item is null) return NotFound();
        _context.HealthPersonas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id:int}/compartir/{usuarioId:int}")]
    public async Task<IActionResult> Compartir(int id, int usuarioId)
    {
        var userId = this.CurrentUserId();
        if (!await TieneAcceso(id, userId)) return Forbid();
        if (!await _context.Usuarios.AnyAsync(u => u.Id == usuarioId)) return NotFound("Usuario no existe");

        if (!await _context.UsuarioHealthPersonas.AnyAsync(uhp => uhp.IdUsuario == usuarioId && uhp.IdHealthPersona == id))
        {
            _context.UsuarioHealthPersonas.Add(new UsuarioHealthPersona { IdUsuario = usuarioId, IdHealthPersona = id });
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
