using System.ComponentModel.DataAnnotations;

namespace Data.Db;

public class Item1Table
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Job { get; set; }
    [Required]
    public short? Age { get; set; }
}