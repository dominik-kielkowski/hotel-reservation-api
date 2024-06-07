using Core.Entities.Rooms;

namespace Application.Hotels
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public string PostalCode { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
