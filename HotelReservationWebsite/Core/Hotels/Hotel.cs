using Core.Common;
using Core.Hotels.Rooms;

namespace Core.Hotels
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
