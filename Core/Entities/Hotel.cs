using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public string Descryption {  get; set; }
        public Address Address { get; set; }
        public Room Rooms { get; set; }
    }
}
