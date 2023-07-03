using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "O campo de nome é obrigatório!")]
        public string NomeCinema { get; set; }
    }
}
