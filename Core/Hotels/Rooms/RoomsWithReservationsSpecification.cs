using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hotels.Rooms
{
    public class RoomsWithReservationsSpecification : BaseSpecification<Room>
    {
        public RoomsWithReservationsSpecification(int id)
        : base(x => x.Id == id)
        {
            AddInclude(x => x.Reservations);
        }
    }
}
