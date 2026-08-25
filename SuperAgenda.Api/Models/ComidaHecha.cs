using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("comidas_hechas")]
public class ComidaHecha
{
    public int Id { get; set; }

    [Column("id_combo")]
    public int IdCombo { get; set; }

    [Column("fecha_hora")]
    public DateTime FechaHora { get; set; }

    [Column("id_health_persona")]
    public int IdHealthPersona { get; set; }
}
