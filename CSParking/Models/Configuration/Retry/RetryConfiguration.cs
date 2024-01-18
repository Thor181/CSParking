using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Models.Configuration.Retry
{
    public class RetryConfiguration
    {
        public int RetryCount { get; set; }
        public int RetryDelayMilliseconds { get; set; }
    }
}
