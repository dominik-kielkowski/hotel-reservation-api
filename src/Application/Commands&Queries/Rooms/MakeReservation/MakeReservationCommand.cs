using Core.Entities.Rooms;
using MediatR;
using System.Windows.Input;

namespace Application.Hotels.Rooms.MakeReservation
{
    public sealed record MakeReservationCommand(int roomId, Guid userId, Reservation reservation) : IRequest<MakeReservationResult>;

    public sealed record MakeReservationResult(bool IsSuccess);
}