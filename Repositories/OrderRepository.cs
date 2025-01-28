using myresto.Models;
using Microsoft.EntityFrameworkCore;
using myresto.Data;

namespace myresto.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(MyRestoContext context) : base(context)
        {
        }
    }
}
