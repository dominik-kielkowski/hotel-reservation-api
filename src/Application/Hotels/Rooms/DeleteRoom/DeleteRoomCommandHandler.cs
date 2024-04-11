using Application.Hotels.Rooms.GetRoomById;
using Core.Common;
using Core.Hotels.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hotels.Rooms.DeleteRoom
{
    public sealed class DeleteRoomCommandHandler
    {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteRoomCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
            {
                var room = await _unitOfWork.Repository<Room>().GetByIdAsync(query.roomId);

                if (room == null)
                {
                    throw new InvalidOperationException("Room does not exhist");
                }

                _unitOfWork.Repository<Room>().Delete(room);

                return Unit.Value;
            }
    }
}
