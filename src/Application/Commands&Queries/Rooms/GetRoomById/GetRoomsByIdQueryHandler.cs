using Application.Hotels.Rooms.GetRooms;
using Core.Common;
using Core.Entities.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hotels.Rooms.GetRoomById
{
    public sealed class GetRoomsByIdQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomsByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RoomDto> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(query.roomId);

            if (room == null)
            {
                throw new InvalidOperationException("Room does not exhist");
            }

            return room.ToDto();
        }
    }
}
