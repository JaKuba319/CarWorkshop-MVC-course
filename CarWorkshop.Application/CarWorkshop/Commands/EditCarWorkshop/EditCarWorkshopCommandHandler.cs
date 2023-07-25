using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICarWorkshopRepository _repository;

        public EditCarWorkshopCommandHandler(IMapper mapper, ICarWorkshopRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Unit> Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.CarWorkshop>(request);

            await _repository.EditCarWorkshop(entity);

            return Unit.Value;
        }
    }
}
