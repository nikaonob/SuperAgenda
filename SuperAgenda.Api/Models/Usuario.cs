using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("usuario")]
public class Usuario
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Password { get; set; }
}
