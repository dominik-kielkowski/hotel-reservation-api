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
        private readonly IUnitOfWork _unitOfWork;

        public RoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return  (await _unitOfWork.Repository<Room>().GetByIdAsync(id)).ToDto();
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
    }
}
