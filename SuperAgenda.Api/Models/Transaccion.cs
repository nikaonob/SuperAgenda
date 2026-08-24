using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("Transacciones")]
public class Transaccion
{
    public int Id { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Monto { get; set; }

    public int DesdeID { get; set; }

    public int HaciaID { get; set; }

    [Required, MaxLength(200)]
    public string Comentario { get; set; } = string.Empty;

    [Column("date")]
    public DateTime Date { get; set; }

    public int DolarID { get; set; }

    [Required, MaxLength(1)]
    public string Moneda { get; set; } = string.Empty;

    public DateTime? DatePago { get; set; }
}
