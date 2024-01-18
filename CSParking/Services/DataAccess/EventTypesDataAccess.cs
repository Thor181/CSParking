using CSParking.Models.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.DataAccess
{
    public class EventTypesDataAccess : BaseDataAccess
    {
        public EventTypesDataAccess(CSParkingContext db, IConfiguration configuration, ILogger<BaseDataAccess> logger) : base(db, configuration, logger)
        {
        }

        public async Task<int> GetEventTypeIdAsync(string name)
        {
            var type = await DbContext.EventsTypes.SingleOrDefaultAsync(x => Equals(x.Name, name));

            if (type == null)
            {
                Logger.LogError("Event type with name \"{name}\" not found", name);
                return -1;
            }

            return type.Id;
        }
    }
}
