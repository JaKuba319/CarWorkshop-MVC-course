using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Domain.Interfaces
{
    public interface ICarWorkshopServiceRepository
    {
        Task Create(CarWorkshopService service);
        Task<IEnumerable<CarWorkshopService>> GetServicesByEncodedName(string encodedName);
    }
}