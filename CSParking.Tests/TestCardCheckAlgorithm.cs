using CSParking.Models.Database;
using CSParking.Services.Algorithms.Card;
using CSParking.Services.DataAccess;
using CSParking.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.EntityFrameworkCore;

namespace CSParking.Tests
{
    public class TestCardCheckAlgorithm
    {
        private readonly IServiceProvider serviceProvider;
        private IConfiguration configuration;
        private CardCheckAlgorithm algo;

        public TestCardCheckAlgorithm()
        {
            var core = new Core();
            serviceProvider = core.ServiceProvider;
            configuration = core.ServiceProvider.GetRequiredService<IConfiguration>();
            algo = core.ServiceProvider.GetRequiredService<CardCheckAlgorithm>();
        }

        [Fact]
        public async Task CardNotFound()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckCardDto("thiscardisfake", 1, 1));
            var closeResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            Assert.Equal(closeResponse, response);
        }

        [Fact]
        public async Task BeforeFieldIsExpired()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckCardDto("Card1", 1, 1));
            var closeResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            Assert.Equal(closeResponse, response);
        }

        [Fact]
        public async Task PlaceIdNotEqualCom()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckCardDto("Card2", 2, 1));
            var openResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);
            Assert.Equal(openResponse, response);
        }

        [Fact]
        public async Task PlaceIdEqualCom_UserIsNotStaff()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckCardDto("Card3", 1, 1));
            var closeResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            Assert.Equal(closeResponse, response);
        }

        [Fact]
        public async Task PlaceIdEqualCom_UserIsStaff()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckCardDto("Card4", 1, 1));
            var openResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);
            Assert.Equal(openResponse, response);
        }
    }
}