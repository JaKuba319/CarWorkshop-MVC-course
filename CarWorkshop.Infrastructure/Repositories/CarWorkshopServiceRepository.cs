using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Persistence;
using CarWorkshop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<CarWorkshopService>> GetServicesByEncodedName(string encodedName)
         => await _dbContext.Services
            .Where(s => s.CarWorkshop.EncodedName == encodedName).ToListAsync();
    }
}
