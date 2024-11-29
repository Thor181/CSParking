using Azure.Core;
using CSParking.Services.DataAccess.Main;
using CSParking.Utils;
using CSParking.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Controllers
{
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly CountDataAccess _countDataAccess;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public CountController(CountDataAccess countDataAccess, ILogger<CountController> logger, IConfiguration configuration)
        {
            _countDataAccess = countDataAccess;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/ticetplace/check")]
        public async Task<string> CheckTicketCountAsync()
        {
            var count = await _countDataAccess.GetCurrentTicketCountAsync();

            return ResponseHelper.Format(count.ToString());
        }

        [HttpGet]
        [Route("/ticetplace/in")]
        public async Task<string> ProcessTicketInAsync()
        {
            var closeResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            var openResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);
            var closeResponseIfLessThanZero = _configuration.GetNeedCloseResponseIfLessThanZero();

            try
            {
                await _countDataAccess.DecrementTicketCountAsync(closeResponseIfLessThanZero);

                return openResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while trying to process Ticket In");

                return closeResponse;
            }
        }

        [HttpGet]
        [Route("/ticetplace/out")]
        public async Task<string> ProcessTicketOutAsync()
        {
            var closeResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            var openResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);

            try
            {
                await _countDataAccess.IncrementTicketCountAsync();

                return openResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while trying to process Ticket In");

                return closeResponse;
            }
        }

        [HttpGet]
        [Route("/cardtplace/check")]
        public async Task<string> CheckCardCountAsync()
        {
            var count = await _countDataAccess.GetCurrentCardCountAsync();

            return ResponseHelper.Format(count.ToString());
        }

        [HttpGet]
        [Route("/cardtplace/in")]
        public async Task<string> ProcessCardInAsync()
        {
            var closeResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            var openResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);
            var closeResponseIfLessThanZero = _configuration.GetNeedCloseResponseIfLessThanZero();

            try
            {
                await _countDataAccess.DecrementCardCountAsync(closeResponseIfLessThanZero);

                return openResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while trying to process Card In");

                return closeResponse;
            }
        }

        [HttpGet]
        [Route("/cardtplace/out")]
        public async Task<string> ProcessCardOutAsync()
        {
            var closeResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            var openResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);

            try
            {
                await _countDataAccess.IncrementCardCountAsync();

                return openResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while trying to process Card Out");

                return closeResponse;
            }
        }
    }
}
