﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.Common.Exceptions
{
    [Serializable]
    public class NoCarbonFootprintDataException : Exception
    {
        public NoCarbonFootprintDataException() : base("Household consumption data not found.") { }
    }
}