using CSParking.Services.Algorithms.Card;
using CSParking.Services.Algorithms.Qr;
using CSParking.Utils.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Tests
{
    public class TestQrCheckAlgorithm
    {
        private readonly IServiceProvider serviceProvider;
        private IConfiguration configuration;
        private QrCheckAlgorithm algo;

        public TestQrCheckAlgorithm()
        {
            var core = new Core();
            serviceProvider = core.ServiceProvider;
            configuration = core.ServiceProvider.GetRequiredService<IConfiguration>();
            algo = core.ServiceProvider.GetRequiredService<QrCheckAlgorithm>();
        }

        [Fact]
        public async Task QrEventWithPayCashlessFound()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckQrEventDto("1234567", 1));
            var closeResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            Assert.Equal(closeResponse, response);
        }

        [Fact]
        public async Task QrEventNotFound_Any()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckQrEventDto("000000", 1));
            var closeResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            Assert.Equal(closeResponse, response);
        }

        [Fact]
        public async Task QrEventWithPayTypeCashFound()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckQrEventDto("2234567", 1));
            var openResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);
            Assert.Equal(openResponse, response);
        }

        [Fact]
        public async Task QrEventWithPayTypeCashNotFound_QrEventWithEmptyPayTypeFound_TimeNotMore()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckQrEventDto("3234567", 1));
            var openResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);
            Assert.Equal(openResponse, response);
        }

        [Fact]
        public async Task QrEventWithPayTypeCashNotFound_QrEventWithEmptyPayTypeFound_TimeMore()
        {
            var response = await algo.GetResponseAsync(new Models.Web.CheckQrEventDto("9994567", 1));
            var closeResponse = configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            Assert.Equal(closeResponse, response);
        }
    }
}
