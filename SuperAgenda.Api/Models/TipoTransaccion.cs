using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("tipo_transaccion")]
public class TipoTransaccion
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Grupo { get; set; } = string.Empty;
}
