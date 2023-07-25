using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName
{
    public class GetCarWorkshopByEncodedNameQuerryHandler : IRequestHandler<GetCarWorkshopByEncodedNameQuerry, CarWorkshopDto>
    {
        private readonly ICarWorkshopRepository _repository;
        private readonly IMapper _mapper;

        public GetCarWorkshopByEncodedNameQuerryHandler(ICarWorkshopRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CarWorkshopDto> Handle(GetCarWorkshopByEncodedNameQuerry request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _repository.GetByEncodedName(request.EncodedName);
            var dto = _mapper.Map<CarWorkshopDto>(carWorkshop);
            return dto;
        }
    }
}
