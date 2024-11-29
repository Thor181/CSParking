using CSParking.Services.DataAccess.Main;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Tests
{
    public class TestCount
    {
        private readonly CountDataAccess _countDataAccess;

        public TestCount()
        {
            var core = new Core();
            _countDataAccess = core.ServiceProvider.GetRequiredService<CountDataAccess>();
        }
        //TODO: need to finish it
        [Fact]
        public async Task Ticket_SuccessIncrement()
        {
            var prevValue = await _countDataAccess.GetCurrentTicketCountAsync();
            await _countDataAccess.IncrementTicketCountAsync();
            var expectedValue = prevValue + 1;
            var current = await _countDataAccess.GetCurrentTicketCountAsync();

            Assert.Equal(expectedValue, current);
        }

        [Fact]
        public async Task Ticket_SuccessDecrement()
        {
            var prevValue = await _countDataAccess.GetCurrentTicketCountAsync();
            await _countDataAccess.DecrementTicketCountAsync(false);
            var expectedValue = prevValue - 1;
            var current = await _countDataAccess.GetCurrentTicketCountAsync();

            Assert.Equal(expectedValue, current);
        }
    }
}
