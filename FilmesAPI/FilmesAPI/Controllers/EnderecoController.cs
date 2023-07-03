using AutoMapper;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscarEnderecoPorId), new { id = endereco.Id }, enderecoDto);
        }

        [HttpGet]
        public IEnumerable<ReadEnderecoDto> VisualizarEndereco() //IEnumerable deixa o método mais abstrato, e torna uma futura mudança do List indiferente.
        {
            return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.ToList());
        }

        [HttpGet("{id}")] //Para diferenciar do GET de cima, pois este requere um parametro
        public IActionResult BuscarEnderecoPorId(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(endereco);
                return Ok(cinemaDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco == null) return NotFound();

            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

            if (endereco == null) return NotFound();

            _context.Remove(endereco);
            _context.SaveChanges();

            return NoContent();
        }
    }


}

