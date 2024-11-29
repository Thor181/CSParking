using CSParking.Models.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Models.Database.MfRA
{
    public class EventType : IDatabaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
