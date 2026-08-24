using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("medidas")]
public class Medida
{
    public int Id { get; set; }

    [Column("id_health_persona")]
    public int IdHealthPersona { get; set; }

    [Required, MaxLength(50)]
    public string Tipo { get; set; } = string.Empty;

    [Column(TypeName = "decimal(6,2)")]
    public decimal Valor { get; set; }

    [Column(TypeName = "date")]
    public DateTime Fecha { get; set; }
}
