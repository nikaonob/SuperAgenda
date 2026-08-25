using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("comida")]
public class Comida
{
    public int Id { get; set; }

    [Required, MaxLength(80)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(40)]
    public string? Categoria { get; set; }

    [Column("calorias_100g", TypeName = "decimal(6,2)")]
    public decimal Calorias100g { get; set; }

    [Column("gramos_por_unidad", TypeName = "decimal(6,2)")]
    public decimal? GramosPorUnidad { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }
}
