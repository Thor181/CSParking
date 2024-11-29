using CSParking.Models.Database.CsParking.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.DataAccess.Main
{
    public class CountDataAccess
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly IServiceProvider _serviceProvider;

        public CountDataAccess(IServiceProvider serviceProvider)
        {
            _semaphore = new SemaphoreSlim(1);
            _serviceProvider = serviceProvider;
        }

        public async Task<int> GetCurrentTicketCountAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MainContext>();

                var places = await context.Places.SingleAsync();
                var count = places.TicketCount;

                return count;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task IncrementTicketCountAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MainContext>();

                var places = await context.Places.SingleAsync();
                places.TicketCount++;

                await context.SaveChangesAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task DecrementTicketCountAsync(bool throwIfLessThanZero)
        {
            await _semaphore.WaitAsync();

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MainContext>();

                var places = await context.Places.SingleAsync();
                places.TicketCount--;

                if (throwIfLessThanZero && places.TicketCount < 0)
                    throw new InvalidOperationException("TicketCount lesst than zero");

                await context.SaveChangesAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<int> GetCurrentCardCountAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MainContext>();

                var places = await context.Places.SingleAsync();
                var count = places.CardCount;

                return count;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task IncrementCardCountAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MainContext>();

                var places = await context.Places.SingleAsync();
                places.CardCount++;

                await context.SaveChangesAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task DecrementCardCountAsync(bool throwIfLessThanZero)
        {
            await _semaphore.WaitAsync();

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MainContext>();

                var places = await context.Places.SingleAsync();
                places.CardCount--;

                if (throwIfLessThanZero && places.CardCount < 0)
                    throw new InvalidOperationException("CardCount less than zero");

                await context.SaveChangesAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
