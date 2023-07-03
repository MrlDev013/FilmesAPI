using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route ("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto); //Conversão implicita entre filmeDto para Filme. Utilizando extensão AutoMapper.
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscarFilmePorId), new { id = filme.ID }, filme);
    }

    [HttpGet]
    public IEnumerable<ReadFilmeDto> VisualizarFilmes([FromQuery]int skip = 0, [FromQuery]int take = 50) //IEnumerable deixa o método mais abstrato, e torna uma futura mudança do List indiferente.
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }

    [HttpGet("{id}")] //Para diferenciar do GET de cima, pois este requere um parametro
    public IActionResult BuscarFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);

        if(filme == null) return NotFound();

        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

        return Ok(filmeDto);
    }
    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme =>filme.ID == id);

        if (filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);

        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);

        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }
}
