using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Data;

public class AgendaDbContext : DbContext
{
    public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
    {
    }

    public DbSet<Evento> Eventos => Set<Evento>();
}
