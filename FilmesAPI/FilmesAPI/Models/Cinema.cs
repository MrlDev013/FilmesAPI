using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage ="O campo de nome é obrigatório!")]
    public string NomeCinema { get; set; }
    public int EnderecoID { get; set; }
    public virtual Endereco Endereco { get; set; } //Gera o relacionamento 1-1 no banco, entre Cinema e Endereco.
}
