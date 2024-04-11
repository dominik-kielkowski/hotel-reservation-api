using MediatR;
using Core.Common;
using Core.Hotels.Rooms;
using Core.Hotels;

namespace Application.Hotels.Rooms.AddRoom
{
    public sealed class AddRoomCommandHandler : IRequestHandler<AddRoomCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddRoomCommand request, CancellationToken cancellationToken)
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
