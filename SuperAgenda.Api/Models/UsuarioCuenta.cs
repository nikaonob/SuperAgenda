using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("usuario_cuenta")]
public class UsuarioCuenta
{
    public int Id { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_cuenta")]
    public int IdCuenta { get; set; }
}
