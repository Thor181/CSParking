using CSParking.Models.Database.Context;
using CSParking.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.DataAccess
{
    public class PayTypesDataAccess : BaseDataAccess
    {
        public PayTypesDataAccess(CSParkingContext db, IConfiguration configuration, ILogger<BaseDataAccess> logger) 
            : base(db, configuration, logger)
        {
        }

        public async Task<int> GetEmptyPayTypeId()
        {
            var emptyPayTypeValue = Configuration.GetPayTypeName(Utils.Extensions.ConfigurationExtensions.PayType.EmptyPayTypeValue);
            var emptyPayType = await DbContext.PayTypes.SingleOrDefaultAsync(x => Equals(x.Name, emptyPayTypeValue));

            if (emptyPayType == null)
            {
                Logger.LogError("Empty pay type not found");
                return 0;
            }

            var id = emptyPayType.Id;
            return id;
        }

        public async Task<int> GetPayTypeByName(string name)
        {
            var payType = await DbContext.PayTypes.SingleOrDefaultAsync(x => Equals(x.Name, name));

            if (payType == null)
            {
                Logger.LogError("Pay type with name \"{name}\" not found", name);
                return -1;
            }

            return payType.Id;
        }
    }
}
