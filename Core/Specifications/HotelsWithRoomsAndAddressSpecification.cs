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

        public HotelsWithRoomsAndAddressSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Address);
            AddInclude(x => x.Rooms);
        }

        public HotelsWithRoomsAndAddressSpecification()
        {
            AddInclude(x => x.Address);
            AddInclude(x => x.Rooms);
        }
    }
}
