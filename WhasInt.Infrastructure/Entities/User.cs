﻿using Infrastructure.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhasInt.Infrastructure.Entities
{
    public class User : Entity
    {
        public virtual string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}
