using Microsoft.EntityFrameworkCore;
using SuperAgenda.Api.Models;

namespace SuperAgenda.Api.Data;

public class AgendaDbContext : DbContext
{
    public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
    {
    }

    public DbSet<Evento> Eventos => Set<Evento>();
    public DbSet<Entidad> Entidades => Set<Entidad>();
    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<TipoDeMoneda> TiposDeMoneda => Set<TipoDeMoneda>();
    public DbSet<DolarHoy> DolarHoy => Set<DolarHoy>();
    public DbSet<Transaccion> Transacciones => Set<Transaccion>();
    public DbSet<Agent> Agents => Set<Agent>();
    public DbSet<HealthPersona> HealthPersonas => Set<HealthPersona>();
    public DbSet<HealthRegistro> HealthRegistros => Set<HealthRegistro>();
    public DbSet<TypeModel> TypeModels => Set<TypeModel>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Medida> Medidas => Set<Medida>();
    public DbSet<AppVersion> AppVersions => Set<AppVersion>();
    public DbSet<TipoMedida> TiposMedida => Set<TipoMedida>();
    public DbSet<Ejercicio> Ejercicios => Set<Ejercicio>();
    public DbSet<EjercicioPeso> EjercicioPesos => Set<EjercicioPeso>();
    public DbSet<TipoComida> TiposComida => Set<TipoComida>();
    public DbSet<Comida> Comidas => Set<Comida>();
    public DbSet<ComboComida> CombosComida => Set<ComboComida>();
    public DbSet<ComboComidaDetalle> ComboComidaDetalles => Set<ComboComidaDetalle>();
    public DbSet<ComidaHecha> ComidasHechas => Set<ComidaHecha>();
    public DbSet<ComidaDia> ComidasDia => Set<ComidaDia>();
    public DbSet<MenuPlanificado> MenuPlanificado => Set<MenuPlanificado>();
    public DbSet<MenuCompartido> MenuCompartidos => Set<MenuCompartido>();
    public DbSet<UsuarioCuenta> UsuarioCuentas => Set<UsuarioCuenta>();
    public DbSet<UsuarioHealthPersona> UsuarioHealthPersonas => Set<UsuarioHealthPersona>();
    public DbSet<UsuarioConfiguracion> UsuarioConfiguraciones => Set<UsuarioConfiguracion>();
    public DbSet<RutinaDia> RutinaDias => Set<RutinaDia>();
    public DbSet<RutinaDiaEjercicio> RutinaDiaEjercicios => Set<RutinaDiaEjercicio>();
    public DbSet<Tarea> Tareas => Set<Tarea>();
    public DbSet<UsuarioTarea> UsuarioTareas => Set<UsuarioTarea>();
}
