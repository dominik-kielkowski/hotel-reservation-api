using Application.Commands_Queries.Hotels.HotelCommon;
using Core.Common;
using Core.Entities.Hotels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands_Queries.Hotels.DeleteHotel
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHotelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Unit> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Repository<Hotel>().Add(request.Hotel);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
