using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("health_registro")]
public class HealthRegistro
{
    public int Id { get; set; }

    [Column("id_health_persona")]
    public int IdHealthPersona { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public decimal Peso { get; set; }

    [Required, MaxLength(50)]
    public string Tipo { get; set; } = string.Empty;

    [Column(TypeName = "smalldatetime")]
    public DateTime Fecha { get; set; }
}
