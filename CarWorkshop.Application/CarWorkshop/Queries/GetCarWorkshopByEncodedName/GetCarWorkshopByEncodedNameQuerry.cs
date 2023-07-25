using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName
{
    public class GetCarWorkshopByEncodedNameQuerry : IRequest<CarWorkshopDto>
    {
        public GetCarWorkshopByEncodedNameQuerry(string encodedName)
        {
            EncodedName = encodedName;
        }
        public string EncodedName { get; set; } = default!;
    }
}
