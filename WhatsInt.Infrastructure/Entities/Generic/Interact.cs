using Infrastructure.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsInt.Infrastructure.Entities.Generic
{
    public class Interact : Entity
    {
        public string Description { get; set; }

        public int Order { get; set; }

    }
}
