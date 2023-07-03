using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório!")]
    public string NomeCinema { get; set; }
}
