using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whatsint.Model.Generic;

namespace Whatsint.Model
{
    internal class AnswerDto : InteractDto
    {
        public string IdQuestion { get; set; }
        public string IdNextQuestion { get; set; }
    }
}
