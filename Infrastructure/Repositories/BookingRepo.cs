using Infrastructure.Data.Contexts;
using Infrastructure.Data.Entities;

namespace Infrastructure.Repositories;

public class BookingRepo(DataContext context) : BaseRepo<BookingEntity>(context)
{
}
