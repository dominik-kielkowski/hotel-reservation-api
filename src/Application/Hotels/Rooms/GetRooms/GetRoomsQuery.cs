using Application.Common;
using MediatR;

namespace Application.Hotels.Rooms.GetRooms
{
    public sealed record GetRoomsQuery(string? sortBy, string? filter, int? pageNumber, int? pageSize) : IQuery<IReadOnlyList<RoomDto>>;
}
