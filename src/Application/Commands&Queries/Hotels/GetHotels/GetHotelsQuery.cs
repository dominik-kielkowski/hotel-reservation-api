using Application.Common;
using MediatR;

namespace Application.Hotels.GetHotels
{
    public sealed record GetHotelsQuery(string? sortBy, string? filter, int? pageNumber, int? pageSize) : ICachedQuery<IReadOnlyList<HotelDto>>
    {
        public string Key => $"hotels_{sortBy}_{filter}_{pageNumber}_{pageSize}";

        public TimeSpan? Expiration => null;
    }
}
