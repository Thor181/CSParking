using CSParking.Models.Applied;
using CSParking.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Utils.Mapping
{
    public static class QrEventMapping
    {
        public static QrEvent ToQrEvent(this QrEventData eventData)
        {
            return new QrEvent()
            {
                Dt = DateTime.Now,
                FN = eventData.FN,
                PayId = eventData.Pay,
                PointId = eventData.Point, 
                Sum = eventData.Sum,
                TypeId  = eventData.Com,
                FP = eventData.FP
            };
        }

    }
}
