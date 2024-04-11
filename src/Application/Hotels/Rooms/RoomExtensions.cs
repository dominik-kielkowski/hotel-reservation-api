using Core.Hotels.Rooms;

namespace Application.Hotels.Rooms
{
    public static class RoomExtensions
    {
        public static RoomDto ToDto(this Room room)
        {
            if (room == null)
                return null;

            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                RoomSize = room.RoomSize,
                Price = room.Price
            };
        }

        public static Room FromDto(this RoomDto room)
        {
            if (room != null)
                return null;

            return new Room
            {
                Name = room.Name,
                Description = room.Description,
                RoomSize = room.RoomSize,
                Price = room.Price
            };
        }
    }
}