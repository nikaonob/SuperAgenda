using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("tipo_comida")]
public class TipoComida
{
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string Nombre { get; set; } = string.Empty;
}
