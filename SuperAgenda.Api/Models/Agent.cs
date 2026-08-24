using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperAgenda.Api.Models;

[Table("agent")]
public class Agent
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    public DateTime? DateEvent { get; set; }

    public DateTime? CreateOn { get; set; }

    public DateTime? Modific { get; set; }

    public int? RepeatDay { get; set; }

    public int? RepeatMonth { get; set; }

    [MaxLength(20)]
    public string? Type { get; set; }

    public bool Emable { get; set; } = true;
}
