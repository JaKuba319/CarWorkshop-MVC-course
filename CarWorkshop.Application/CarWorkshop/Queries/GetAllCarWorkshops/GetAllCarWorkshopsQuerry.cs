using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops
{
    public class GetAllCarWorkshopsQuerry : IRequest<IEnumerable<CarWorkshopDto>>
    {

    }
}
