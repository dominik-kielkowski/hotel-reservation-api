using Application.Commands_Queries.Hotels;
using Application.Commands_Queries.Hotels.GetHotel;
using Application.Commands_Queries.Hotels.HotelCommon;
using Application.Commands_Queries.Hotels.UpdateHotel;
using Application.Hotels.GetHotels;
using Application.Hotels.Rooms.AddRoom;
using Core.Common;
using Core.Entities.Hotels;
using HotelReservationWebsite.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class HotelController : BaseApiController
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels([FromQuery] GetHotelsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var query = new GetHotelQuery(id);
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel(CreateHotelCommand createHotelCommand)
        {
            return Ok(await _mediator.Send(createHotelCommand));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotel(UpdateHotelCommand updateHotelCommand)
        {
            return Ok(await _mediator.Send(updateHotelCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHotel(DeleteHotelCommand deleteHotelCommand)
        {
            return Ok(await _mediator.Send(deleteHotelCommand));
        }
    }
}
