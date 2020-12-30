﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRandkowy.API.Helpers
{
    public class MessageParams
    {
        public const int MaxPageSize = 48;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 24;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public int UserId { get; set; }
        public string MessageContener { get; set; } = "Nieprzeczytane";
    }
}