using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("menu_compartido")]
public class MenuCompartido
{
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime Fecha { get; set; }

    [Column("id_usuario_origen")]
    public int IdUsuarioOrigen { get; set; }

    [Column("id_usuario_destino")]
    public int IdUsuarioDestino { get; set; }

    [Column("id_health_persona_origen")]
    public int IdHealthPersonaOrigen { get; set; }

    public string Estado { get; set; } = "Pendiente";

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; }
}
