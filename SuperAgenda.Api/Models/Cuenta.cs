using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("cuenta")]
public class Cuenta
{
    public int Id { get; set; }

    public int EntidadID { get; set; }

    [MaxLength(50)]
    public string? NombreCuenta { get; set; }

    public bool Activo { get; set; } = true;

    [Required, MaxLength(1)]
    public string Moneda { get; set; } = "P";
}
