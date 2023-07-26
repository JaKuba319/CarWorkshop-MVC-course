using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System.Net.Http.Headers;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand>
    {
        private readonly ICarWorkshopServiceRepository _serviceRepository;
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IUserContext _userContext;

        public CreateCarWorkshopServiceCommandHandler(ICarWorkshopServiceRepository serviceRepository, ICarWorkshopRepository carWorkshopRepository ,IUserContext userContext)
        {
            _serviceRepository = serviceRepository;
            _userContext = userContext;
            _carWorkshopRepository = carWorkshopRepository;
        }

        public async Task<Unit> Handle(CreateCarWorkshopServiceCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.CarWorkshopEncodedName);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (user.Id == carWorkshop.CreatedById || user.IsInRole("Moderator"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            var carWorkshopService = new Domain.Entities.CarWorkshopService()
            {
                Cost = request.Cost,
                Description = request.Description,
                CarWorkshopId = carWorkshop.Id,
            };
            await _serviceRepository.Create(carWorkshopService);

            return Unit.Value;
        }
    }
}
