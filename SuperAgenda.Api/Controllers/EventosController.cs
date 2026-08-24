using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public EventosController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
    {
        return await _context.Eventos.OrderBy(e => e.FechaInicio).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Evento>> GetEvento(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento is null) return NotFound();
        return evento;
    }

    [HttpPost]
    public async Task<ActionResult<Evento>> PostEvento(Evento evento)
    {
        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEvento), new { id = evento.Id }, evento);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutEvento(int id, Evento evento)
    {
        if (id != evento.Id) return BadRequest();

        _context.Entry(evento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Eventos.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEvento(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento is null) return NotFound();

        _context.Eventos.Remove(evento);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
