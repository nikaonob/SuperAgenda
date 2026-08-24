using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("TipoDeMoneda")]
public class TipoDeMoneda
{
    [Key]
    [MaxLength(1)]
    public string Tipo { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Descripcion { get; set; } = string.Empty;
}
