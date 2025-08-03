using Core.Entities.Rooms;
using MediatR;

namespace HotelReservation.Application.Commands_Queries.Rooms.MakeReservation
{
    public sealed record MakeReservationCommand(int roomId, Guid userId, string userEmail, Reservation reservation) : IRequest<MakeReservationResult>;

    public sealed record MakeReservationResult(bool IsSuccess);
}