using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myresto.Data;
using myresto.Models;
using myresto.Services;

namespace myresto.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;
        private readonly MyRestoContext context;

        public ArticleController(ArticleService articleService, MyRestoContext context)
        {
            _articleService = articleService;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Article>> Get()
        {
            return await _articleService.GetAllArticlesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> Get(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return article;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Article article)
        {
            await _articleService.AddArticleAsync(article);
            return CreatedAtAction(nameof(Get), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            await _articleService.UpdateArticleAsync(article);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _articleService.DeleteArticleAsync(id);
            return NoContent();
        }
    }
}
