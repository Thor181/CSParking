using CSParking.Models.Database.Context;
using CSParking.Models.Web;
using CSParking.Services.Algorithms.Card;
using CSParking.Services.DataAccess;
using CSParking.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Controllers
{
    [ApiController]
    [Route("/card/[action]")]
    public class CardController : ControllerBase
    {
        private readonly CardCheckAlgorithm _cardCheckAlgorithm;

        public CardController(CardCheckAlgorithm cardCheckAlgorithm)
        {
            _cardCheckAlgorithm = cardCheckAlgorithm;
        }

        [HttpGet]
        public async Task<string> CheckCardAsync([FromQuery] CheckCardDto dto)
        {
            var response = await _cardCheckAlgorithm.GetResponseAsync(dto);

            return response;
        }
    }

}
