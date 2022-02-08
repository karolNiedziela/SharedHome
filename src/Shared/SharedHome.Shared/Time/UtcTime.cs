﻿using SharedHome.Shared.Abstractions.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Time
{
    public class UtcTime : ITime
    {
        public DateTime CurrentDate() => DateTime.UtcNow;        
    }
}
