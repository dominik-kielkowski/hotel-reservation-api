using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationWebsite.Controllers
{
    public class HotelController : BaseApiController
    {
        private readonly IGenericRepository<Hotel> _hotelRepo;
        private readonly IGenericRepository<Address> _addressRepo;
        private readonly IGenericRepository<Room> _roomRepo;

        public HotelController(IGenericRepository<Hotel> hotelRepo, IGenericRepository<Address> addressRepo, IGenericRepository<Room> roomRepo)
        {
            _hotelRepo = hotelRepo;
            _addressRepo = addressRepo;
            _roomRepo = roomRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotelByIdAsync(int id)
        {
            var spec = new HotelsWithRoomsAndAddressSpecification(id);

            var hotel = await _hotelRepo.GetEntityWithSpecAsync(spec);

            return hotel;
        }

        [HttpGet]
        public async Task<IReadOnlyList<Hotel>> GetHotelsAsync()
        {
            var spec = new HotelsWithRoomsAndAddressSpecification();

            var hotels = await _hotelRepo.GetEntityListWithSpecAsync(spec);

            return hotels;
        }

        [HttpPost]
        public void AddHotelAsync(Hotel hotel)
        {
            _hotelRepo.Add(hotel);
        }

        [HttpPut]
        public void UpdateHotelAsync(Hotel hotel)
        {
            _hotelRepo.Update(hotel);
        }

        [HttpDelete]
        public void Delete(Hotel hotel)
        {
            _hotelRepo.Delete(hotel);
        }
    }
}
