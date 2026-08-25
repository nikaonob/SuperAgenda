using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("usuario_tarea")]
public class UsuarioTarea
{
    public int Id { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_tarea")]
    public int IdTarea { get; set; }
}
