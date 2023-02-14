using WebApi.Data;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HighscoresController : Controller
    {
        private readonly HighscoreAPIDbContext _dbContext;

        public HighscoresController(HighscoreAPIDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetHighscores()
        {
            if (_dbContext.Highscores == null)
            {
                return NotFound();
            }
            return Json(await _dbContext.Highscores.ToListAsync());
        }

        [HttpGet("top10")]
        public async Task<IActionResult> GetTop10()
        {
            if (_dbContext.Highscores == null)
            {
                return NotFound();
            }
            return Json(await _dbContext.Highscores.Take(10).OrderBy(sorting => sorting.Time).ToListAsync());
        }

        //[HttpGet("{id}")]
        // public async Task<IActionResult> GetHighscoreById(int id)
        // {
        //     var highscoreFinder = await _dbContext.Highscores.FirstOrDefaultAsync(score => score.Id == id);
        //     if (highscoreFinder == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(highscoreFinder);
        // }

        [HttpGet("{username}")] // behöver ses över då Uri blir fel i swagger. (dubbla matchpoints med {id})
        public async Task<IActionResult> GetUserByName(string username)
        {
            var userFinder = await _dbContext.Highscores.Where(find => find.Name.ToLower() == username.ToLower()).FirstOrDefaultAsync();
            if (userFinder == null)
            {
                return NotFound();
            }
            return Json(userFinder);
        }

        [HttpPost]
        public async Task<IActionResult> AddHighscore([FromBody] Highscore highscore)
        {
            var score = new Highscore()
            {
                Id = highscore.Id,
                Name = highscore.Name,
                Time = highscore.Time
            };
            await _dbContext.Highscores.AddAsync(score);
            await _dbContext.SaveChangesAsync();
            return Json(score);
        }

        [HttpDelete]
        [Route("{id}")]//får den bara att fungera med hjälp av ett id.
        public async Task<IActionResult> DeleteHighscoreById(int id)
        {
            var _highscore = await _dbContext.Highscores.FirstOrDefaultAsync(score => score.Id == id);
            if (_highscore == null)
            {
                return NotFound($"Highscore with Id = {id} not found");
            }

            _dbContext.Highscores.RemoveRange(_highscore);
            await _dbContext.SaveChangesAsync();
            return Ok($"Highscore with Id: {id} deleted.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllHighscores()
        {
            if (_dbContext.Highscores.Any() == true)
            {
                var Highscores = await _dbContext.Highscores.ToListAsync();
                foreach (var highscore in Highscores)
                {
                    _dbContext.Highscores.Remove(highscore);
                }
                await _dbContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}