using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("DolarHoy")]
public class DolarHoy
{
    public int Id { get; set; }

    [Column(TypeName = "money")]
    public decimal Dolar { get; set; }

    [Column(TypeName = "date")]
    public DateTime Fecha { get; set; }
}
