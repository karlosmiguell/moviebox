using System.ComponentModel.DataAnnotations;

namespace MovieBox.Models;

public class Filme
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string Categoria { get; set; } = string.Empty;

    [Range(0, 10)]
    public decimal Nota { get; set; }
}
