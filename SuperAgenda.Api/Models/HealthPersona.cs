using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("health_persona")]
public class HealthPersona
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? NombrePersona { get; set; }
}
