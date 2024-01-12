using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class RoomsSpecificationParameters
    {
        public int PageIndex { get; set; } = 1;
        public string Sort { get; set; }
    }
}
