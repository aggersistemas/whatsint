using Infrastructure.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsInt.Infrastructure.Entities.Generic
{
    public class Answer : Interact
    {
        public string IdQuestion { get; set; }
        public string IdNextQuestion { get; set; }
    }
}
