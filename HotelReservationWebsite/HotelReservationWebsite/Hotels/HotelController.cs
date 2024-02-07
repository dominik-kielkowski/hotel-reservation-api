
using Core.Common;
using Core.Hotels;
using HotelReservationWebsite.Common;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationWebsite.Hotels
{
    public class HotelController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Hotel> _genericRepository;

        public HotelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotelById(int id)
        {
            var spec = new HotelsWithRoomsAndAddressSpecification(id);

            var hotel = await _unitOfWork.Repository<Hotel>().GetEntityWithSpecAsync(spec);

            return hotel.ToDto();
        }

        [HttpGet]
        public async Task<IReadOnlyList<HotelDto>> GetHotels([FromQuery] HotelsSpecificationParameters parameters)
        {
            var spec = new HotelsWithRoomsAndAddressSpecification(parameters);

            var hotels = await _unitOfWork.Repository<Hotel>().GetEntityListWithSpecAsync(spec);

            return hotels.Select(x => x.ToDto()).ToList();
        }

        [HttpPost]
        public async Task AddHotel(Hotel hotel)
        {
            _unitOfWork.Repository<Hotel>().Add(hotel);
            await _unitOfWork.Complete();
        }

        [HttpPut]
        public async Task UpdateHotel(Hotel hotel)
        {
            _unitOfWork.Repository<Hotel>().Update(hotel);
            await _unitOfWork.Complete();
        }

        [HttpDelete]
        public async Task DeleteHotel(Hotel hotel)
        {
            _unitOfWork.Repository<Hotel>().Delete(hotel);
            await _unitOfWork.Complete();
        }
    }
}
