using myresto.Models;
using Microsoft.EntityFrameworkCore;
using myresto.Data;

namespace myresto.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository(MyRestoContext context) : base(context)
        {
        }
    }
}
