using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    public class CreateEnderecoDto
    {
        [Required(ErrorMessage = "O logradouro é obrigatório!")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O número do endereço é obrigatório")]
        public int Numero { get; set; }
    }
}
