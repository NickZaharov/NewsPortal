using Microsoft.AspNetCore.Mvc;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchNews([FromQuery] NewsQueryFilter filter)
        {
            var news = await _newsService.Search(filter);

            if (news.Count == 0)
                return NotFound("No news found");

            return Ok(news);
        }
    }
}
