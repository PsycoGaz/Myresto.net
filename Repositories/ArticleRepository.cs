using myresto.Models;
using myresto.Data;

namespace myresto.Repositories
{
    public class ArticleRepository : GenericRepository<Article>
    {
        public ArticleRepository(MyRestoContext context) : base(context)
        {
        }
    }
}
