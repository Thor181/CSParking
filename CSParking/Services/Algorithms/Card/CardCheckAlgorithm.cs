using CSParking.Models.Applied;
using CSParking.Models.Database;
using CSParking.Models.Web;
using CSParking.Services.DataAccess.CsParking;
using CSParking.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.Algorithms.Card
{
    public class CardCheckAlgorithm
    {
        private readonly UsersDataAccess _usersDataAccess;
        private readonly CardEventsDataAccess _cardEventsDataAccess;
        private readonly IConfiguration _configuration;

        public CardCheckAlgorithm(UsersDataAccess usersDataAccess, CardEventsDataAccess cardEventsDataAccess, IConfiguration configuration)
        {
            _usersDataAccess = usersDataAccess;
            _cardEventsDataAccess = cardEventsDataAccess;
            _configuration = configuration;
        }

        public async Task<string> GetResponseAsync(CheckCardDto checkCardDto)
        {
            var closeResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            var openResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);

            var user = await _usersDataAccess.GetUserByCardAsync(checkCardDto.Card);

            if (user == null)
                return closeResponse;

            if (DateTime.Now >= user.Before)
                return closeResponse;

            if (user.PlaceId == checkCardDto.Com)
            {
                if (user.Staff != true)
                    return closeResponse;
            }

            var changingPlaceIsSuccess = await _usersDataAccess.ChangeUserPlace(checkCardDto.Card, checkCardDto.Com);

            if (!changingPlaceIsSuccess)
                return closeResponse;

            await _cardEventsDataAccess.WriteAsync(checkCardDto);

            return openResponse;
        }
    }
}
