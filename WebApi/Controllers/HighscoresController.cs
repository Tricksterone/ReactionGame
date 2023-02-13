using WebApi.Data;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Json(await _dbContext.Highscores.ToListAsync());
        }

        [HttpGet]
        [Route("top10")] // behöver se över så endast 10 tas in.
        public async Task<IActionResult> GetTop10()
        {
            return Json(await _dbContext.Highscores.Take(10).OrderBy(sort => sort.Time).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddHighscore([FromBody]Highscore highscore)
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
    }
}