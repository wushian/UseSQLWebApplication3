﻿using Cus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cus.Interfaces.Order
{
    public interface IEmployee
    {
        IEnumerable<Employees> GetEmployees();
    }
}
