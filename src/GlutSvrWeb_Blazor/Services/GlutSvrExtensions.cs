using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvr.Services
{
    public static class GlutSvrExtensions
    {
        public static decimal GetPercent(this decimal value, int totalItems)
        {
            if(totalItems <= 0)
            {
                return 0M;
            }
            return ((value * 100) / totalItems);
        }
    }
}
