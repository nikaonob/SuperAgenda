using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("usuario_configuracion")]
public class UsuarioConfiguracion
{
    public int Id { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required, MaxLength(10)]
    public string Color { get; set; } = "#4F46E5";

    [Column("menu_principal"), MaxLength(20)]
    public string? MenuPrincipal { get; set; }
}
