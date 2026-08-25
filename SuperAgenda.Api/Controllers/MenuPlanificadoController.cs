using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuPlanificadoController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public MenuPlanificadoController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuPlanificado>>> GetAll() => await _context.MenuPlanificado.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MenuPlanificado>> Get(int id)
    {
        var item = await _context.MenuPlanificado.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<MenuPlanificado>> Post(MenuPlanificado item)
    {
        var existente = await _context.MenuPlanificado.FirstOrDefaultAsync(m =>
            m.Fecha == item.Fecha && m.IdTipoComida == item.IdTipoComida && m.IdHealthPersona == item.IdHealthPersona);

        if (existente is not null)
        {
            existente.IdCombo = item.IdCombo;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = existente.Id }, existente);
        }

        _context.MenuPlanificado.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.MenuPlanificado.FindAsync(id);
        if (item is null) return NotFound();
        _context.MenuPlanificado.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
