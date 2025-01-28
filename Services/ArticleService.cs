using myresto.Models;
using myresto.Repositories;


namespace myresto.Services
{
    public class ArticleService
    {
        private readonly ArticleRepository _articleRepository;

        public ArticleService(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _articleRepository.GetAllAsync();
        }

        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _articleRepository.GetByIdAsync(id);
        }

        public async Task AddArticleAsync(Article article)
        {
            await _articleRepository.AddAsync(article);
        }

        public async Task UpdateArticleAsync(Article article)
        {
            await _articleRepository.UpdateAsync(article);
        }

        public async Task DeleteArticleAsync(int id)
        {
            await _articleRepository.DeleteAsync(id);
        }
    }
}
