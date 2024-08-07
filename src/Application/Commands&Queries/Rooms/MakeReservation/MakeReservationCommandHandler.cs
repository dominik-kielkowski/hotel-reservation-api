using Core.Common;
using Core.Entities.Rooms;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Hotels.Rooms.MakeReservation
{
    public sealed class MakeReservationCommandHandler : IRequestHandler<MakeReservationCommand, MakeReservationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        public MakeReservationCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<MakeReservationResult> Handle(MakeReservationCommand command, CancellationToken cancellationToken)
        {
            var reservationBegin = command.reservation.Begin.ToUniversalTime();
            var reservationEnd = command.reservation.End.ToUniversalTime();
            var room = await _unitOfWork.Repository<Room>().AccessContext().Include(r => r.Reservations).SingleOrDefaultAsync(r => r.Id == command.roomId);

            if (room == null)
            {
                throw new ArgumentException("Room does not exist");
            }

            if (room.Reservations.Any(r => (reservationBegin >= r.Begin && reservationBegin <= r.End) || (reservationEnd >= r.Begin && reservationEnd <= r.End)))
            {
                throw new ArgumentException("There is a conflicting reservation for the chosen time slot.");
            }

            var reservation = new Reservation
            {
                Begin = reservationBegin,
                End = reservationEnd,
                CustomerId = command.userId,
                RoomId = command.roomId
            };

            _unitOfWork.Repository<Reservation>().Add(reservation);

            await _unitOfWork.SaveChangesAsync();

            var reservationConfirmedEvent = new ReservationConfirmedEvent(reservation.Id, command.userId, command.roomId, reservationBegin, reservationEnd);
            await _publishEndpoint.Publish(reservationConfirmedEvent, cancellationToken);

            return new MakeReservationResult(true);
        }
    }
}
