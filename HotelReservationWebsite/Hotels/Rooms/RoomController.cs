using Core.Common;
using Core.Hotels.Rooms;
using Core.User;
using HotelReservationWebsite.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace HotelReservationWebsite.Hotels.Rooms
{
    public class RoomController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<AppUser> _userManager;

        public RoomController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IReadOnlyList<RoomDto>> GetRooms([FromQuery] RoomsSpecificationParameters parameters)
        {
            var spec = new RoomsWithFiltersSpecification(parameters);

            var rooms = await _unitOfWork.Repository<Room>().GetEntityListWithSpecAsync(spec);

            return rooms.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<RoomDto> GetRoomById(int id)
        {
            return (await _unitOfWork.Repository<Room>().GetByIdAsync(id)).ToDto();
        }

        [HttpPost]
        public async Task AddRoomToHotel(Room room)
        {
            _unitOfWork.Repository<Room>().Add(room);
            await _unitOfWork.Complete();
        }

        [HttpDelete]
        public async Task DeleteRoom(Room room)
        {
            _unitOfWork.Repository<Room>().Delete(room);
            await _unitOfWork.Complete();
        }

        [HttpPut]
        public async Task UpdateRoom(Room room)
        {
            _unitOfWork.Repository<Room>().Update(room);
            await _unitOfWork.Complete();
        }

        [HttpPost]
        [Authorize]
        [Route("{roomId}/reservations")]
        public async Task<IActionResult> MakeReservation(int roomId, Reservation reservation)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (customerId == null || await _userManager.FindByIdAsync(customerId) == null)
            {
                return BadRequest("Unauthorized");
            }

            var room = await _unitOfWork.Repository<Room>().GetEntityWithSpecAsync(new RoomsWithReservationsSpecification(roomId));
            if (room == null)
            {
                return NotFound("Room not found.");
            }

            var reservationBegin = reservation.Begin.ToUniversalTime();
            var reservationEnd = reservation.End.ToUniversalTime();

            if (room.Reservations.Any(r => (reservationBegin >= r.Begin && reservationBegin <= r.End) || (reservationEnd >= r.Begin && reservationEnd <= r.End)))
            {
                return Conflict("There is a conflicting reservation for the chosen time slot.");
            }

            _unitOfWork.Repository<Reservation>().Add(new Reservation
            {
                Begin = reservationBegin,
                End = reservationEnd,
                CustomerId = Guid.Parse(customerId),
                RoomId = roomId
            });

            await _unitOfWork.Complete();

            return Ok("Reservation successfully created.");
        }

    }
}
