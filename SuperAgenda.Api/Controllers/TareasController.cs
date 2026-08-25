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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetAll()
    {
        var userId = this.CurrentUserId();
        return await _context.Tareas.Where(t => t.IdUsuario == userId).OrderBy(t => t.Fecha).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Tarea>> Get(int id)
    {
        var userId = this.CurrentUserId();
        var item = await _context.Tareas.FindAsync(id);
        if (item is null || item.IdUsuario != userId) return NotFound();
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
        if (existente is null || existente.IdUsuario != userId) return NotFound();

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
        if (item is null || item.IdUsuario != userId) return NotFound();

        _context.Tareas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
