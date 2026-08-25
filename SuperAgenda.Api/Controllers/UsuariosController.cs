using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;
using SuperAgenda.Api.Models.Auth;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AgendaDbContext _context;
    private readonly PasswordHasher<Usuario> _hasher = new();

    public UsuariosController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetAll() => await _context.Usuarios.ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Usuario>> Get(int id)
    {
        var item = await _context.Usuarios.FindAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> Post(LoginRequest request)
    {
        var item = new Usuario { Name = request.Name };
        item.Password = _hasher.HashPassword(item, request.Password);

        _context.Usuarios.Add(item);
        await _context.SaveChangesAsync();

        var persona = new HealthPersona { NombrePersona = item.Name };
        _context.HealthPersonas.Add(persona);
        await _context.SaveChangesAsync();

        _context.UsuarioHealthPersonas.Add(new UsuarioHealthPersona { IdUsuario = item.Id, IdHealthPersona = persona.Id });
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Usuario item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Usuarios.AnyAsync(e => e.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Usuarios.FindAsync(id);
        if (item is null) return NotFound();
        _context.Usuarios.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
