using CSParking.Models.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Models.Database.MfRA
{
    public class QrEvent : IDatabaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Dt { get; set; }
        public int TypeId { get; set; }
        public int PointId { get; set; }
        public int PayId { get; set; }
        public decimal Sum { get; set; }
        public string FN { get; set; }
        public string FP { get; set; }
    }
}
