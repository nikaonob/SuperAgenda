using System.ComponentModel.DataAnnotations;

namespace SuperAgenda.Api.Models;

public class Evento
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Descripcion { get; set; }

    [Required]
    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    [MaxLength(200)]
    public string? Lugar { get; set; }

    public bool Completado { get; set; } = false;

    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
}
