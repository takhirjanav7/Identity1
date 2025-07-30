using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Bll.Services.Helper
{
    public static class TimeHelper
    {
        public static DateTime GetDateTime()
        {
            var dtTime = DateTime.UtcNow;
            dtTime.AddHours(5);
            return dtTime;
        }
    }
}
