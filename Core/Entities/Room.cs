namespace Core.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RoomSize { get; set; }
    }
}