using CSParking.Models.Applied;
using CSParking.Models.Database;
using CSParking.Models.Database.Context;
using CSParking.Utils.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.DataAccess
{
    public class QrEventsDataAccess : BaseDataAccess
    {
        public QrEventsDataAccess(CSParkingContext db, IConfiguration configuration, ILogger<BaseDataAccess> logger) 
            : base(db, configuration, logger)
        {
        }

        public async Task WriteAsync(QrEventData qrEventData)
        {
            await WriteAsync<QrEvent>(qrEventData.ToQrEvent());
        }

        public async Task WriteAsync(QrEvent qrEvent)
        {
            await WriteAsync<QrEvent>(qrEvent);
        }

        public async Task<QrEvent?> GetQrEvent(string fn, int payType)
        {
            var qrEvent = await DbContext.QrEvents.FirstOrDefaultAsync(x => Equals(x.FN, fn) && x.PayId == payType);
            return qrEvent;
        }

        public async Task<QrEvent> GetQrEvent(string fn)
        {
            var qrEvent = await DbContext.QrEvents.FirstOrDefaultAsync(x => Equals(x.FN, fn));
            return qrEvent;
        }
    }
}
