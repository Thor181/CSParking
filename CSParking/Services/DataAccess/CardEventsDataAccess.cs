using CSParking.Models.Applied;
using CSParking.Models.Database;
using CSParking.Models.Database.Context;
using CSParking.Models.Web;
using CSParking.Utils.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.DataAccess
{
    public class CardEventsDataAccess : BaseDataAccess
    {
        public CardEventsDataAccess(CSParkingContext db, IConfiguration configuration, ILogger<CardEventsDataAccess> logger)
            : base(db, configuration, logger)
        {

        }

        public async Task WriteAsync(CheckCardDto checkCardDto)
        {
            await WriteAsync(checkCardDto.ToCardEvent());
        }
    }
}
