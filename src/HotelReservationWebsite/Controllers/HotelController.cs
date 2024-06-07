using Application.Hotels;
using Application.Hotels.GetHotels;
using Core.Common;
using Core.Entities.Hotels;
using HotelReservationWebsite.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class HotelController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public HotelController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotelById(int id)
        {
            var hotel = await _unitOfWork.Repository<Hotel>()
                                         .AccessContext()
                                         .Include(h => h.Address)
                                         .Include(h => h.Rooms)
                                         .SingleOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound("Hotel not found");
            }

            return Ok(hotel.ToDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels([FromQuery] GetHotelsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task CreateHotel(Hotel hotel)
        {
            _unitOfWork.Repository<Hotel>().Add(hotel);
            await _unitOfWork.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateHotel(Hotel hotel)
        {
            _unitOfWork.Repository<Hotel>().Update(hotel);
            await _unitOfWork.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task DeleteHotel(Hotel hotel)
        {
            _unitOfWork.Repository<Hotel>().Delete(hotel);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
