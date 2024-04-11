using Core.Common;
using Core.Hotels.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Hotels.Rooms.GetRooms
{
    public sealed class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, IReadOnlyList<RoomDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<RoomDto>> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
        {
            var queryable = _unitOfWork.Repository<Room>().AccessContext().AsQueryable();

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

            var rooms = await queryable.Select(room => room.ToDto()).ToListAsync();

            return rooms;
        }
    }
}
