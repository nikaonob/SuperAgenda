using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuCompartidoController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public MenuCompartidoController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuCompartido>>> GetAll()
    {
        var userId = this.CurrentUserId();
        return await _context.MenuCompartidos
            .Where(m => m.IdUsuarioOrigen == userId || m.IdUsuarioDestino == userId)
            .OrderByDescending(m => m.FechaCreacion)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<MenuCompartido>> Post(MenuCompartido item)
    {
        var userId = this.CurrentUserId();
        item.IdUsuarioOrigen = userId;
        item.Estado = "Pendiente";
        item.FechaCreacion = DateTime.UtcNow;

        _context.MenuCompartidos.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), item);
    }

    [HttpPost("{id:int}/aceptar")]
    public async Task<IActionResult> Aceptar(int id)
    {
        var userId = this.CurrentUserId();
        var compartido = await _context.MenuCompartidos.FindAsync(id);
        if (compartido is null) return NotFound();
        if (compartido.IdUsuarioDestino != userId) return Forbid();
        if (compartido.Estado != "Pendiente") return BadRequest("Este plan ya fue respondido.");

        var personaDestino = await ResolverPersonaDestinoAsync(userId);
        if (personaDestino is null) return BadRequest("No se encontró una persona de Health para asignar el plan.");

        var planesOrigen = await _context.MenuPlanificado
            .Where(p => p.Fecha == compartido.Fecha && p.IdHealthPersona == compartido.IdHealthPersonaOrigen)
            .ToListAsync();

        foreach (var plan in planesOrigen)
        {
            var existente = await _context.MenuPlanificado.FirstOrDefaultAsync(p =>
                p.Fecha == compartido.Fecha && p.IdTipoComida == plan.IdTipoComida && p.IdHealthPersona == personaDestino.Value);

            if (existente is not null)
            {
                existente.IdCombo = plan.IdCombo;
            }
            else
            {
                _context.MenuPlanificado.Add(new MenuPlanificado
                {
                    Fecha = compartido.Fecha,
                    IdTipoComida = plan.IdTipoComida,
                    IdCombo = plan.IdCombo,
                    IdHealthPersona = personaDestino.Value
                });
            }
        }

        compartido.Estado = "Aceptado";
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id:int}/rechazar")]
    public async Task<IActionResult> Rechazar(int id)
    {
        var userId = this.CurrentUserId();
        var compartido = await _context.MenuCompartidos.FindAsync(id);
        if (compartido is null) return NotFound();
        if (compartido.IdUsuarioDestino != userId) return Forbid();
        if (compartido.Estado != "Pendiente") return BadRequest("Este plan ya fue respondido.");

        compartido.Estado = "Rechazado";
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = this.CurrentUserId();
        var compartido = await _context.MenuCompartidos.FindAsync(id);
        if (compartido is null) return NotFound();
        if (compartido.IdUsuarioOrigen != userId) return Forbid();

        _context.MenuCompartidos.Remove(compartido);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private async Task<int?> ResolverPersonaDestinoAsync(int userId)
    {
        var config = await _context.UsuarioConfiguraciones.FirstOrDefaultAsync(c => c.IdUsuario == userId);
        if (config?.IdHealthPersonaDefault is int idDefault) return idDefault;

        return await _context.UsuarioHealthPersonas
            .Where(uhp => uhp.IdUsuario == userId)
            .Select(uhp => (int?)uhp.IdHealthPersona)
            .FirstOrDefaultAsync();
    }
}
