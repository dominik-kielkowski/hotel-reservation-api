using Core.Common;
using Core.Entities.Rooms;

namespace Core.Entities.Hotels
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
