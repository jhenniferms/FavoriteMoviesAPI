using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FavoriteMoviesAPI.Data;
using FavoriteMoviesAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FavoriteMoviesAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar filmes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MoviesController(AppDbContext context, IConfiguration configuration, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtém todos os filmes.
        /// </summary>
        /// <returns>Uma lista de filmes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        /// <summary>
        /// Obtém um filme pelo ID.
        /// </summary>
        /// <param name="id">ID do filme.</param>
        /// <returns>O filme correspondente ao ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            return movie;
        }

        /// <summary>
        /// Busca filmes na OMDb API.
        /// </summary>
        /// <param name="title">Título do filme.</param>
        /// <returns>Informações do filme.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchMovie([FromQuery] string title)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest("O título do filme é obrigatório.");

            var apiKey = _configuration["OMDb:ApiKey"];
            var baseUrl = _configuration["OMDb:BaseUrl"];
            var response = await _httpClient.GetAsync($"{baseUrl}?t={title}&apikey={apiKey}");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Erro ao buscar filme na OMDb API.");

            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        /// <summary>
        /// Adiciona um novo filme.
        /// </summary>
        /// <param name="movie">Dados do filme.</param>
        /// <returns>O filme criado.</returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        /// <summary>
        /// Atualiza um filme existente.
        /// </summary>
        /// <param name="id">ID do filme.</param>
        /// <param name="movie">Dados atualizados do filme.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
                return BadRequest("ID do filme inválido.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Movies.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um filme.
        /// </summary>
        /// <param name="id">ID do filme.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}



