using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("combo_comida_detalle")]
public class ComboComidaDetalle
{
    public int Id { get; set; }

    [Column("id_combo")]
    public int IdCombo { get; set; }

    [Column("id_comida")]
    public int IdComida { get; set; }

    [Column("cantidad_gramos", TypeName = "decimal(6,2)")]
    public decimal CantidadGramos { get; set; }
}
