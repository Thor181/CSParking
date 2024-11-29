using CSParking.Models.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Models.Database.MfRA
{
    public class CardEvent : IDatabaseEntity
    {
        public int Id { get; set; }
        public DateTime Dt { get; set; }
        public int TypeId { get; set; }
        public int PointId { get; set; }
        public string Card { get; set; }
    }
}
