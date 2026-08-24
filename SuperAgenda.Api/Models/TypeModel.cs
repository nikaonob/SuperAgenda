using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("typeModels")]
public class TypeModel
{
    public int Id { get; set; }

    [Column(TypeName = "char(3)")]
    public string? Idmodels { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Description { get; set; }
}
