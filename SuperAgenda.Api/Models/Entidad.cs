using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("entidad")]
public class Entidad
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Nombre { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }
}
