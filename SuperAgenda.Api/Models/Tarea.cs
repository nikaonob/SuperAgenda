using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("tarea")]
public class Tarea
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Descripcion { get; set; }

    public DateTime Fecha { get; set; }

    [Column("es_alarma")]
    public bool EsAlarma { get; set; }

    [Required, MaxLength(20)]
    public string Estado { get; set; } = "Pendiente";

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }
}
