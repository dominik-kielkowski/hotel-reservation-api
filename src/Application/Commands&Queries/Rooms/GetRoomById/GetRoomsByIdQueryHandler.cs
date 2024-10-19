using Core.Common;
using Core.Entities.Rooms;
using MediatR;

namespace Application.Hotels.Rooms.GetRoomById
{
    public sealed class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RoomDto> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(query.roomId);

            if (room == null)
            {
                throw new InvalidOperationException("Room does not exist");
            }

            return room.ToDto();
        }
    }
}
