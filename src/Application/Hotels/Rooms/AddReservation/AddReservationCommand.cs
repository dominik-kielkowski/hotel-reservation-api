using Core.Hotels.Rooms;
using MediatR;
using System.Windows.Input;

namespace Application.Hotels.Rooms.AddReservation
{
    public sealed record AddReservationCommand(int roomId, Guid userId, Reservation reservation) : IRequest;
}