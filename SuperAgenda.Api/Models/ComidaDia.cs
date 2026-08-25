using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("comida_dia")]
public class ComidaDia
{
    public int Id { get; set; }

    [Column("id_comida")]
    public int IdComida { get; set; }

    [Column("cantidad_gramos", TypeName = "decimal(6,2)")]
    public decimal CantidadGramos { get; set; }

    [Column("fecha_hora")]
    public DateTime FechaHora { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }
}
