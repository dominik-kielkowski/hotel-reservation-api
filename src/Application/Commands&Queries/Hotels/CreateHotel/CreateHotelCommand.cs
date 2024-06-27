using Core.Entities.Hotels;
using MediatR;

namespace Application.Commands_Queries.Hotels.HotelCommon
{
    public record CreateHotelCommand(Hotel Hotel) : IRequest;
}
