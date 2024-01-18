using CSParking.Models.Applied;
using CSParking.Models.Database;
using CSParking.Models.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Utils.Mapping
{
    public static class CardEventMapping
    {
        public static CardEvent ToCardEvent(this CardEventData eventData)
        {
            return new CardEvent()
            {
                Dt = DateTime.Now,
                Card = eventData.Card,
                PointId = eventData.Point,
                TypeId = eventData.Com
            };
        }

        public static CardEvent ToCardEvent(this CheckCardDto checkCardDto)
        {
            return new CardEvent()
            {
                Dt = DateTime.Now,
                Card = checkCardDto.Card,
                PointId = checkCardDto.Point,
                TypeId = checkCardDto.Com
            };
        }

    }
}
