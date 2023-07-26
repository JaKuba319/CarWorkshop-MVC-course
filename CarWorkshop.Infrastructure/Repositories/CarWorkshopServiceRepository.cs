using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Persistence;
using CarWorkshop.Domain.Interfaces;
namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarWorkshopServiceRepository : ICarWorkshopServiceRepository
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopServiceRepository(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(CarWorkshopService service)
        {
            _dbContext.Services.Add(service);
            await _dbContext.SaveChangesAsync();
        }
    }
}
