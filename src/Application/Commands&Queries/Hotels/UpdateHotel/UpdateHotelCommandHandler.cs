using Core.Common;
using Core.Entities.Hotels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands_Queries.Hotels.UpdateHotel
{
    internal class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHotelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Repository<Hotel>().Update(request.Hotel);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
