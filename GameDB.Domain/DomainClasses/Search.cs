﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDB.Domain.DomainClasses
{
    public class Search
    {
        public List<Game> Games { get; set; }
        public List<User> Users { get; set; }
    }
}
