using Core.Common;

namespace Core.Entities.Rooms
{
    public class Room : BaseEntity
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RoomSize { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}