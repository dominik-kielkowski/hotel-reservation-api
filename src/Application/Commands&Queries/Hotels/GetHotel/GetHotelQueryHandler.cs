using Application.Commands_Queries.Hotels.GetHotel;
using Application.Hotels;
using Core.Common;
using Core.Entities.Hotels;
using MediatR;
using Microsoft.EntityFrameworkCore;

public sealed class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, HotelDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetHotelQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<HotelDto> Handle(GetHotelQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _unitOfWork.Repository<Hotel>()
                                     .AccessContext()
                                     .Include(h => h.Address)
                                     .Include(h => h.Rooms)
                                     .SingleOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

        if (hotel == null)
        {
            throw new NotFoundException("Hotel not found");
        }

        return hotel.ToDto();
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}