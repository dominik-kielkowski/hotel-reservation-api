using Core.Common;

namespace Core.Hotels.Rooms
{
    public class RoomsWithFiltersSpecification : BaseSpecification<Room>
    {
        private const int pageSize = 15;
        public RoomsWithFiltersSpecification(RoomsSpecificationParameters roomParameters)
        {
            ApplyPaging(pageSize * (roomParameters.PageIndex - 1), pageSize);

            if (!string.IsNullOrEmpty(roomParameters.Sort))
            {
                switch (roomParameters.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "roomSizeAsc":
                        AddOrderBy(p => p.RoomSize);
                        break;
                    case "roomSizeDesc":
                        AddOrderByDescending(p => p.RoomSize);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }
    }
}
