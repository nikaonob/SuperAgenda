using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SuperAgenda.Api.Models;

[Table("usuario")]
public class Usuario
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    [JsonIgnore]
    public string? Password { get; set; }
}
