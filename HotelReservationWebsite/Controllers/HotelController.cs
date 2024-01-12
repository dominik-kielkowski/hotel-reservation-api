using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using HotelReservationWebsite.Dtos;
using HotelReservationWebsite.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationWebsite.Controllers
{
    public class HotelController : BaseApiController
    {
        private readonly IGenericRepository<Hotel> _hotelRepo;

        public HotelController(IGenericRepository<Hotel> hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotelById(int id)
        {
            var spec = new HotelsWithRoomsAndAddressSpecification(id);

            var hotel = await _hotelRepo.GetEntityWithSpecAsync(spec);

            return hotel.ToDto();
        }

        [HttpGet]
        public async Task<IReadOnlyList<HotelDto>> GetHotels([FromQuery] HotelsSpecificationParameters parameters)
        {
            var spec = new HotelsWithRoomsAndAddressSpecification(parameters);

            var hotels = await _hotelRepo.GetEntityListWithSpecAsync(spec);

            return hotels.Select(x => x.ToDto()).ToList();
        }

        [HttpPost]
        public void AddHotel(Hotel hotel)
        {
            _hotelRepo.Add(hotel);
        }

        [HttpPut]
        public void UpdateHotel(Hotel hotel)
        {
            _hotelRepo.Update(hotel);
        }

        [HttpDelete]
        public void DeleteHotel(Hotel hotel)
        {
            _hotelRepo.Delete(hotel);
        }
    }
}
