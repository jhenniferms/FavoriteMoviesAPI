using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FavoriteMoviesAPI.Data;
using FavoriteMoviesAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteMoviesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/1/favorites
        [HttpGet("{userId}/favorites")]
        public async Task<ActionResult<IEnumerable<FavoriteMovie>>> GetFavorites(int userId)
        {
            var user = await _context.Users
                .Include(u => u.FavoriteMovies)
                .ThenInclude(fm => fm.Movie) // Carrega os dados do filme
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user.FavoriteMovies);
        }
    }
}
