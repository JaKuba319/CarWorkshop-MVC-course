using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandHandler : IRequestHandler<CreateCarWorkshopCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICarWorkshopRepository _repository;
        private readonly IUserContext _userContext;

        public CreateCarWorkshopCommandHandler(IMapper mapper, ICarWorkshopRepository repository, IUserContext userContext) 
        {
            _mapper = mapper;
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(CreateCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if(currentUser == null || !currentUser.IsInRole("Owner"))
            {
                return Unit.Value;
            }
            var carWorkshop = _mapper.Map<Domain.Entities.CarWorkshop>(request);
            carWorkshop.EncodeName();

            carWorkshop.CreatedById = currentUser.Id;

            await _repository.Create(carWorkshop);

            return Unit.Value;
        }
    }
}
