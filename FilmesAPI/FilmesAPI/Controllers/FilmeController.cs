using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route ("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;


    //HttpPost serve para a ação 'Post', que adiciona o filme que recebemos por parametro.
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        filme.ID = id++; 
        filmes.Add(filme);
        return CreatedAtAction(nameof(BuscaFilmePorId), new { id = filme.ID }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> VisualizarFilmes([FromQuery]int skip = 0, [FromQuery]int take = 50) //IEnumerable deixa o método mais abstrato, e torna uma futura mudança do List indiferente.
    {
        return filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")] //Para diferenciar do GET de cima, pois este requere um parametro
    public IActionResult BuscaFilmePorId(int id)
    {
        var filme = filmes.FirstOrDefault(filme => filme.ID == id);

        if(filme == null) return NotFound();

        return Ok();
    }
}
