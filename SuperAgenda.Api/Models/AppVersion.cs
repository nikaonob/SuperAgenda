using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("app_version")]
public class AppVersion
{
    public int Id { get; set; }

    [Column("version_minima")]
    public int VersionMinima { get; set; }

    public string? Mensaje { get; set; }
}
