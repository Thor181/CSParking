using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Models.Applied
{
    public record QrEventData(string FN, int Com, int Point, decimal Sum, int Pay, string FP);
}
