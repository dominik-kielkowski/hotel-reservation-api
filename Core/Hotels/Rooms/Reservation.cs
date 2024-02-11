using Core.Common;

namespace Core.Hotels.Rooms
{
    public class Reservation : BaseEntity
    {
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Guid CustomerId { get; set; }
        public int RoomId { get; set; }
    }
}
