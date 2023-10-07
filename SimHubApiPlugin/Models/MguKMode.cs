using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimHubApiPlugin.Models
{
    public enum MguKMode
    {
        Charging = 0,
        BalancedLow = 1,
        BalancedHigh = 2,
        Overtake = 3,
        TopSpeed = 4,
        Hotlap = 5
    }
}
