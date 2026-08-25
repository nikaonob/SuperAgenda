using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MiConfiguracionController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public MiConfiguracionController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<UsuarioConfiguracion>> Get()
    {
        var userId = this.CurrentUserId();
        var config = await _context.UsuarioConfiguraciones.FirstOrDefaultAsync(c => c.IdUsuario == userId);

        if (config is null)
        {
            config = new UsuarioConfiguracion { IdUsuario = userId, Color = "#4F46E5", MenuPrincipal = null };
            _context.UsuarioConfiguraciones.Add(config);
            await _context.SaveChangesAsync();
        }

        return config;
    }

    [HttpPut]
    public async Task<ActionResult<UsuarioConfiguracion>> Put(UsuarioConfiguracion request)
    {
        var userId = this.CurrentUserId();
        var config = await _context.UsuarioConfiguraciones.FirstOrDefaultAsync(c => c.IdUsuario == userId);

        if (config is null)
        {
            config = new UsuarioConfiguracion { IdUsuario = userId };
            _context.UsuarioConfiguraciones.Add(config);
        }

        config.Color = string.IsNullOrWhiteSpace(request.Color) ? "#4F46E5" : request.Color;
        config.MenuPrincipal = request.MenuPrincipal;
        config.IdHealthPersonaDefault = request.IdHealthPersonaDefault;

        await _context.SaveChangesAsync();
        return config;
    }
}
