using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specyfications;
using HotelReservationWebsite.Dtos;
using HotelReservationWebsite.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationWebsite.Controllers
{
    public class RoomController : BaseApiController
    {
        private readonly IGenericRepository<Room> _roomRepo;

        public RoomController(IGenericRepository<Room> roomRepo)
        {
            _roomRepo = roomRepo;
        }

        [HttpGet]
        public async Task<IReadOnlyList<RoomDto>> GetRooms([FromQuery] RoomsSpecificationParameters parameters)
        {
            var spec = new RoomsWithFiltersSpecification(parameters);

            var rooms = await _roomRepo.GetEntityListWithSpecAsync(spec);

            return rooms.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<RoomDto> GetRoomById(int id)
        {
            return  (await _roomRepo.GetByIdAsync(id)).ToDto();
        }

        [HttpPost]
        public void AddRoomToHotel(Room room)
        {
            _roomRepo.Add(room);
        }

        [HttpDelete]
        public void DeleteRoom(Room room)
        {
            _roomRepo.Delete(room);
        }
    }
}
