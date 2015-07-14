using Cus.Interfaces.Order;
using Cus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cus.Services.Order
{
    public class ShipperService
    {
        private IShipper _Shipper = null;
        public ShipperService(IShipper Shipper)
        {
            _Shipper = Shipper;
        }

        public IEnumerable<Shippers> GetShippers()
        {
            return _Shipper.GetShippers();
        }
    }
}
