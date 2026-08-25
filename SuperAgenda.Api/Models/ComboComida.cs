using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("combo_comida")]
public class ComboComida
{
    public int Id { get; set; }

    [Column("id_tipo_comida")]
    public int IdTipoComida { get; set; }

    public string? Nombre { get; set; }

    [Column(TypeName = "date")]
    public DateTime Fecha { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }
}
