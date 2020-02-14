﻿using System;
using System.Globalization;

namespace DonVo.Services.ActualResults
{
    public class Error
    {
        public string Time { get; set; }
        public string Description { get; set; }

        public Error(DateTime time, string description)
        {
            Time = time.ToString(CultureInfo.InvariantCulture);
            Description = description;
        }
    }
}
