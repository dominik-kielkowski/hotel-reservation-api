using Core.Entities.Rooms;
using MediatR;
using System.Windows.Input;

namespace Application.Hotels.Rooms.AddReservation
{
    public sealed record MakeReservationCommand(int roomId, Guid userId, Reservation reservation) : IRequest;
}