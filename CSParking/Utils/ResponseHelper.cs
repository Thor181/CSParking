using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Utils
{
    public static class ResponseHelper
    {
        public static string Format(string body)
        {
            return $"@{body}%";
        }
    }
}
