using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Enums
{
    public enum ProjectStatus:int
    {
        Pending = 1,
        Completed = 2,
        OnHold = 3,
        Canceled = 4,
    }
}
