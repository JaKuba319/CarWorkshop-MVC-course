using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Repositories
{
    internal class CarWorkshopRepository : ICarWorkshopRepository
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopRepository(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(Domain.Entities.CarWorkshop carWorkshop)
        {
            _dbContext.Add(carWorkshop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entities.CarWorkshop>> GetAll()
            => await _dbContext.CarWorkshops.ToListAsync();

        public Task<Domain.Entities.CarWorkshop?> GetByName(string name)
            => _dbContext.CarWorkshops.FirstOrDefaultAsync(cw => cw.Name.ToLower() == name.ToLower());

        public async Task<Domain.Entities.CarWorkshop> GetByEncodedName(string encodedName)
            => await _dbContext.CarWorkshops.Where(x => x.EncodedName == encodedName).FirstAsync();

        public async Task EditCarWorkshop(Domain.Entities.CarWorkshop entity)
        {
            var oldEntity = await GetByEncodedName(entity.EncodedName);
            oldEntity.Description = entity.Description;
            oldEntity.About = entity.About;
            oldEntity.ContactDetails.PhoneNumber = entity.ContactDetails.PhoneNumber;
            oldEntity.ContactDetails.Street = entity.ContactDetails.Street;
            oldEntity.ContactDetails.City = entity.ContactDetails.City;
            oldEntity.ContactDetails.PostalCode = entity.ContactDetails.PostalCode;
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
