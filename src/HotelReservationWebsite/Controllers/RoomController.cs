using Application.Hotels.Rooms.AddReservation;
using Application.Hotels.Rooms.AddRoom;
using Application.Hotels.Rooms.DeleteRoom;
using Application.Hotels.Rooms.GetRoomById;
using Application.Hotels.Rooms.GetRooms;
using Application.Hotels.Rooms.UpdateRoom;
using Core.Entities.Rooms;
using HotelReservationWebsite.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class RoomController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] GetRoomsQuery query)
        {
            return Ok(await _mediator.Send(query));     
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(GetRoomByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomToHotel(CreateRoomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(DeleteRoomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom(UpdateRoomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        [Authorize]
        [Route("{roomId}/reservations")]
        public async Task<IActionResult> MakeReservation(int roomId, Reservation reservation, CancellationToken cancellationToken)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new MakeReservationCommand(roomId, Guid.Parse(customerId), reservation);

            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}
