using Application.Hotels.Rooms;
using Application.Hotels.Rooms.AddReservation;
using Application.Hotels.Rooms.AddRoom;
using Application.Hotels.Rooms.DeleteRoom;
using Application.Hotels.Rooms.GetRoomById;
using Application.Hotels.Rooms.GetRooms;
using Application.Hotels.Rooms.UpdateRoom;
using Core.Common;
using Core.Hotels.Rooms;
using Core.User;
using HotelReservationWebsite.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HotelReservationWebsite.Hotels.Rooms
{
    public class RoomController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public RoomController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMediator mediator)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] GetRoomsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(GetRoomByIdQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoomToHotel(AddRoomCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(DeleteRoomCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom(UpdateRoomCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [Route("{roomId}/reservations")]
        public async Task<IActionResult> MakeReservation(int roomId, Reservation reservation)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (customerId == null || _userManager.FindByIdAsync(customerId) == null)
            {
                return BadRequest("Unauthorized");
            }

            var command = new AddReservationCommand(roomId, Guid.Parse(customerId), reservation);
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
