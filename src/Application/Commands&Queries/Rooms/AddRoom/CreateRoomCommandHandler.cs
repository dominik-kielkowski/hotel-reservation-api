using MediatR;
using Core.Common;
using Core.Entities.Rooms;
using Core.Entities.Hotels;

namespace Application.Hotels.Rooms.AddRoom
{
    public sealed class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var hotel = _unitOfWork.Repository<Hotel>().GetByIdAsync(request.room.HotelId).Result;
            if (hotel == null)
            {
                throw new Exception("Hotel does not exhist");
            }

            _unitOfWork.Repository<Room>().Add(request.room);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
