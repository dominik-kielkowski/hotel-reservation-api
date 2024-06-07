using Core.Entities.Rooms;
using MediatR;

namespace Application.Hotels.Rooms.AddRoom
{
    public sealed record CreateRoomCommand(Room room) : IRequest;
}
