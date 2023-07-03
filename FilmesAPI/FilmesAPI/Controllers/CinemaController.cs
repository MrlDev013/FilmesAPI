using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    [HttpPost]
    public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto) {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscarCinemasPorId), new{id = cinema.Id}, cinemaDto);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDto> VisualizarCinemas() //IEnumerable deixa o método mais abstrato, e torna uma futura mudança do List indiferente.
    {
        return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
    }

    [HttpGet("{id}")] //Para diferenciar do GET de cima, pois este requere um parametro
    public IActionResult BuscarCinemasPorId(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema != null)
        {
            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }
            
        return NotFound();  
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema == null) return NotFound();

        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema == null) return NotFound();

        _context.Remove(cinema);
        _context.SaveChanges();

        return NoContent();
    }
}

