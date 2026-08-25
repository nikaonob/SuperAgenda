using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("rutina_dia_ejercicio")]
public class RutinaDiaEjercicio
{
    public int Id { get; set; }

    [Column("id_rutina_dia")]
    public int IdRutinaDia { get; set; }

    [Column("id_ejercicio")]
    public int IdEjercicio { get; set; }
}
