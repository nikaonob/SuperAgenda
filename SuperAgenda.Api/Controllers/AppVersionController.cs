using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Data;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AppVersionController : ControllerBase
{
    private readonly AgendaDbContext _context;

    public AppVersionController(AgendaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<AppVersion>> Get()
    {
        var item = await _context.AppVersions.OrderByDescending(v => v.Id).FirstOrDefaultAsync();
        if (item is null) return NotFound();
        return item;
    }
}
