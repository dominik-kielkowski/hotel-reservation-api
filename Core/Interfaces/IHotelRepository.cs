using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IHotelRepository
    {
        public Task<Hotel> CreateHotelAsync();
        public Task<IReadOnlyList<Hotel>> GetHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int id);
        public Task<Address> GetHotelAddressAsync();
        public Task <IReadOnlyList<Room>> GetHotelRoomsAsync();
    }
}
