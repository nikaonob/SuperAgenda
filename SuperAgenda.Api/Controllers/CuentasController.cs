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

    private IQueryable<int> CuentaIdsAccesibles(int userId) =>
        _context.UsuarioCuentas.Where(uc => uc.IdUsuario == userId).Select(uc => uc.IdCuenta);

    private async Task<bool> TieneAcceso(int cuentaId, int userId) =>
        await _context.UsuarioCuentas.AnyAsync(uc => uc.IdUsuario == userId && uc.IdCuenta == cuentaId);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cuenta>>> GetAll()
    {
        var userId = this.CurrentUserId();
        var ids = CuentaIdsAccesibles(userId);
        return await _context.Cuentas.Where(c => ids.Contains(c.Id)).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cuenta>> Get(int id)
    {
        var userId = this.CurrentUserId();
        if (!await TieneAcceso(id, userId)) return Forbid();

        var item = await _context.Cuentas.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Cuenta>> Post(Cuenta item)
    {
        var userId = this.CurrentUserId();
        _context.Cuentas.Add(item);
        await _context.SaveChangesAsync();

        _context.UsuarioCuentas.Add(new UsuarioCuenta { IdUsuario = userId, IdCuenta = item.Id });
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Cuenta item)
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
            if (!await _context.Cuentas.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = this.CurrentUserId();
        if (!await TieneAcceso(id, userId)) return Forbid();

        var item = await _context.Cuentas.FindAsync(id);
        if (item is null) return NotFound();
        _context.Cuentas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id:int}/compartir/{usuarioId:int}")]
    public async Task<IActionResult> Compartir(int id, int usuarioId)
    {
        var userId = this.CurrentUserId();
        if (!await TieneAcceso(id, userId)) return Forbid();
        if (!await _context.Usuarios.AnyAsync(u => u.Id == usuarioId)) return NotFound("Usuario no existe");

        if (!await _context.UsuarioCuentas.AnyAsync(uc => uc.IdUsuario == usuarioId && uc.IdCuenta == id))
        {
            _context.UsuarioCuentas.Add(new UsuarioCuenta { IdUsuario = usuarioId, IdCuenta = id });
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
