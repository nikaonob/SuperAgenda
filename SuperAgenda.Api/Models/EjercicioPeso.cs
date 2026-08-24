using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("ejercicio_pesos")]
public class EjercicioPeso
{
    public int Id { get; set; }

    [Column("id_ejercicio")]
    public int IdEjercicio { get; set; }

    [Column(TypeName = "decimal(6,2)")]
    public decimal Peso { get; set; }

    [Column(TypeName = "date")]
    public DateTime Fecha { get; set; }
}
