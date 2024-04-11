using Core.Common;
using Core.Hotels.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Hotels.Rooms.AddReservation
{
    public sealed class AddReservationCommandHandler : IRequestHandler<AddReservationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddReservationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddReservationCommand command, CancellationToken cancellationToken)
        {
            var reservationBegin = command.reservation.Begin.ToUniversalTime();
            var reservationEnd = command.reservation.End.ToUniversalTime();
            var room = await _unitOfWork.Repository<Room>().AccessContext().Include(r => r.Reservations).SingleOrDefaultAsync(r => r.Id == command.roomId);

            if (room == null)
            {
                throw new ArgumentException("Room does not exhist");
            }

            if (room.Reservations.Any(r => (reservationBegin >= r.Begin && reservationBegin <= r.End) || (reservationEnd >= r.Begin && reservationEnd <= r.End)))
            {
                throw new ArgumentException("There is a conflicting reservation for the chosen time slot.");
            }

            _unitOfWork.Repository<Reservation>().Add(new Reservation
            {
                Begin = reservationBegin,
                End = reservationEnd,
                CustomerId = command.userId,
                RoomId = command.roomId
            });

            var respones = await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
