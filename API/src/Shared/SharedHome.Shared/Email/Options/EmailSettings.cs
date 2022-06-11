﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Email.Options
{
    public class EmailSettings
    {
        public const string SectionName = "Email";

        public string Host { get; set; } = default!;

        public int Port { get; set; }

        public string Address { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}