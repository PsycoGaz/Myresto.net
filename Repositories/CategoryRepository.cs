using myresto.Models;
using myresto.Data;

namespace myresto.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(MyRestoContext context) : base(context)
        {
        }
    }
}
