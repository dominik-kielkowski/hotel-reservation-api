using Core.Common;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Core.Entities.Hotels;

namespace Application.Hotels.GetHotels
{
    public sealed class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, IReadOnlyList<HotelDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHotelsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<HotelDto>> Handle(GetHotelsQuery query, CancellationToken cancellationToken)
        {
            var queryable = _unitOfWork.Repository<Hotel>().AccessContext().Include(h => h.Address).Include(h => h.Rooms).AsQueryable();

            if (!string.IsNullOrEmpty(query.sortBy))
            {
                switch (query.sortBy.ToLower())
                {
                    case "name":
                        queryable = queryable.OrderBy(room => room.Name);
                        break;
                    default:
                        queryable = queryable.OrderBy(room => room.Id);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(query.filter))
            {
                queryable = queryable.Where(room => room.Name.Contains(query.filter));
            }

            var pageNumber = query.pageNumber ?? 1;
            var pageSize = query.pageSize ?? 10;
            var totalCount = await queryable.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            queryable = queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var hotels = await queryable.Select(hotel => hotel.ToDto()).ToListAsync();

            return hotels;
        }
    }
}
