using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public TareasController(AgendaDbContext context)
    {
        _context = context;
    }

    private IQueryable<int> TareaIdsCompartidasConmigo(int userId) =>
        _context.UsuarioTareas.Where(ut => ut.IdUsuario == userId).Select(ut => ut.IdTarea);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetAll()
    {
        var userId = this.CurrentUserId();
        var compartidas = TareaIdsCompartidasConmigo(userId);
        return await _context.Tareas
            .Where(t => t.IdUsuario == userId || compartidas.Contains(t.Id))
            .OrderBy(t => t.Fecha)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Tarea>> Get(int id)
    {
        var userId = this.CurrentUserId();
        var item = await _context.Tareas.FindAsync(id);
        if (item is null) return NotFound();

        var tieneAcceso = item.IdUsuario == userId || await _context.UsuarioTareas.AnyAsync(ut => ut.IdUsuario == userId && ut.IdTarea == id);
        if (!tieneAcceso) return NotFound();

        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Tarea>> Post(Tarea item)
    {
        item.IdUsuario = this.CurrentUserId();
        _context.Tareas.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Tarea item)
    {
        var userId = this.CurrentUserId();
        if (id != item.Id) return BadRequest();

        var existente = await _context.Tareas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        if (existente is null) return NotFound();
        if (existente.IdUsuario != userId) return Forbid();

        item.IdUsuario = userId;
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = this.CurrentUserId();
        var item = await _context.Tareas.FindAsync(id);
        if (item is null) return NotFound();
        if (item.IdUsuario != userId) return Forbid();

        _context.Tareas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id:int}/compartir/{usuarioId:int}")]
    public async Task<IActionResult> Compartir(int id, int usuarioId)
    {
        var userId = this.CurrentUserId();
        var tarea = await _context.Tareas.FindAsync(id);
        if (tarea is null) return NotFound();
        if (tarea.IdUsuario != userId) return Forbid();
        if (!await _context.Usuarios.AnyAsync(u => u.Id == usuarioId)) return NotFound("Usuario no existe");

        if (!await _context.UsuarioTareas.AnyAsync(ut => ut.IdUsuario == usuarioId && ut.IdTarea == id))
        {
            _context.UsuarioTareas.Add(new UsuarioTarea { IdUsuario = usuarioId, IdTarea = id });
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }
}
