using Application.Hotels.Rooms.GetRoomById;
using Core.Common;
using Core.Entities.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hotels.Rooms.UpdateRoom
{
    public sealed class UpdateRoomCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateRoomCommand rqeuest, CancellationToken cancellationToken)
        {
            _unitOfWork.Repository<Room>().Update(rqeuest.room);

            return Unit.Value;
        }
    }
}
