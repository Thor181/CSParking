using CSParking.Models.Database;
using CSParking.Models.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.DataAccess
{
    public class UsersDataAccess : BaseDataAccess
    {
        public UsersDataAccess(CSParkingContext db, IConfiguration configuration, ILogger<UsersDataAccess> logger) 
            : base(db, configuration, logger)
        {
        }

        public async Task<User?> GetUserByCardAsync(string card)
        {
            var user = await DbContext.Users.SingleOrDefaultAsync(x => Equals(x.Card, card));
            return user;
        }

        public async Task<bool> ChangeUserPlace(string card, int place)
        {
            var user = await GetUserByCardAsync(card);

            if (user == null)
            {
                Logger.LogError("User with Card \"{card}\" not found", card);
                return false;
            }

            try
            {
                user.PlaceId = place;
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Saving changes to the database ended with an error");
                return false;
            }
        }

    }
}
