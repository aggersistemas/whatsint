using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsInt.Model.Generic;

namespace WhatsInt.Model
{
    internal class AnswerDto : InteractDto
    {
        public string IdQuestion { get; set; }
        public string IdNextQuestion { get; set; }
    }
}
