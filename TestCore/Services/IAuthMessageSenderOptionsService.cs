﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.Services
{
    public class IAuthMessageSenderOptionsService
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
