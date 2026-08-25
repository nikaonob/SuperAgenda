using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("menu_planificado")]
public class MenuPlanificado
{
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime Fecha { get; set; }

    [Column("id_tipo_comida")]
    public int IdTipoComida { get; set; }

    [Column("id_combo")]
    public int IdCombo { get; set; }

    [Column("id_health_persona")]
    public int IdHealthPersona { get; set; }
}
