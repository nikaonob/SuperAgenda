using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("usuario_health_persona")]
public class UsuarioHealthPersona
{
    public int Id { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_health_persona")]
    public int IdHealthPersona { get; set; }
}
