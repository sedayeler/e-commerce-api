﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class Action
    {
        public string HttpType { get; set; }
        public string ActionType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }
    }
}
