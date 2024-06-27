using Core.Common;
using Core.Entities.Hotels;
using Core.Entities.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands_Queries.Hotels.HotelCommon
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateHotelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Repository<Hotel>().Add(request.Hotel);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
