using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class HotelsWithRoomsAndAddressSpecification : BaseSpecification<Hotel>
    {
        private const int pageSize = 5;
        public HotelsWithRoomsAndAddressSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Address);
            AddInclude(x => x.Rooms);
        }

        public HotelsWithRoomsAndAddressSpecification(HotelsSpecificationParameters hotelParameters)
            : base(x => string.IsNullOrEmpty(hotelParameters.Search) || x.Name.ToLower()
                      .Contains(hotelParameters.Search))
        {
            AddInclude(x => x.Address);
            AddInclude(x => x.Rooms.Take(3));
            AddOrderBy(x => x.Name);
            ApplyPaging(pageSize * (hotelParameters.PageIndex - 1), pageSize);
        }
    }
}
