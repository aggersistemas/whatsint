using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsInt.Model.Generic
{
    public class InteractDto : BaseDto
    {
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
