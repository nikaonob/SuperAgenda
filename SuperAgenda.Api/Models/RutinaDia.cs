using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("rutina_dia")]
public class RutinaDia
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;

    public int Orden { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }
}
