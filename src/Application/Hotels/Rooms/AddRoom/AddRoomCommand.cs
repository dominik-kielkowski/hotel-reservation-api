using Core.Hotels.Rooms;
using MediatR;

namespace Application.Hotels.Rooms.AddRoom
{
    public sealed record AddRoomCommand(Room room) : IRequest;
}
