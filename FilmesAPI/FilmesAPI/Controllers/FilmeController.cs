using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route ("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;

    public FilmeController(FilmeContext context)
    {
        _context = context;
    }

    //HttpPost serve para a ação 'Post', que adiciona o filme que recebemos por parametro.
    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscarFilmePorId), new { id = filme.ID }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> VisualizarFilmes([FromQuery]int skip = 0, [FromQuery]int take = 50) //IEnumerable deixa o método mais abstrato, e torna uma futura mudança do List indiferente.
    {
        return _context.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")] //Para diferenciar do GET de cima, pois este requere um parametro
    public IActionResult BuscarFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);

        if(filme == null) return NotFound();

        return Ok(filme);
    }
}
