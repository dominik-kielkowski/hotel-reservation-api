using Core.Hotels.Rooms;

namespace Core.Hotels
{
    public interface IHotelRepository
    {
        public Task<Hotel> CreateHotelAsync();
        public Task<IReadOnlyList<Hotel>> GetHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int id);
        public Task<Address> GetHotelAddressAsync();
        public Task<IReadOnlyList<Room>> GetHotelRoomsAsync();
    }
}
