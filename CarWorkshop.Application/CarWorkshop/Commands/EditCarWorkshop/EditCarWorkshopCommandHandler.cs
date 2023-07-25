using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICarWorkshopRepository _repository;
        private readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(IMapper mapper, ICarWorkshopRepository repository, IUserContext userContext)
        {
            _mapper = mapper;
            _repository = repository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.CarWorkshop>(request);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && user.Id == entity.CreatedById;

            if(!isEditable)
            {
                return Unit.Value;
            }

            await _repository.EditCarWorkshop(entity);

            return Unit.Value;
        }
    }
}
