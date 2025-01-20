using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Enums
{
    public enum OrderStatus
    {
        Preparing = 0,
        Shipped = 1,
        Delivered = 2,
        Canceled = 3
    }
}
