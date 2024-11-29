using CSParking.Models.Database;
using CSParking.Services.Algorithms.Card;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;
using Castle.Core.Logging;
using Serilog;
using CSParking.Services.Algorithms.Qr;
using CSParking.Models.Database.Base;
using CSParking.Models.Database.CsParking.Context;
using CSParking.Services.DataAccess.CsParking;
using CSParking.Models.Database.CsParking;
using CSParking.Services.DataAccess.Main;

namespace CSParking.Tests
{
    public class Core
    {
        public IServiceProvider ServiceProvider { get; set; }

        public Core()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().AddJsonFile("testappsettings.json").Build();

            var connectionString = configuration.GetConnectionString("MfRADb_Development");

            services.AddScoped<IConfiguration>(_ => configuration);

            services.AddScoped(x =>
            {
                var mockDbContext = new Mock<CSParkingContext>();
                mockDbContext.Setup(x => x.Users).ReturnsDbSet(TestDataHelper.GetFakeUserList());
                mockDbContext.Setup(x => x.QrEvents).ReturnsDbSet(TestDataHelper.GetFakeQrEventList());
                mockDbContext.Setup(x => x.PayTypes).ReturnsDbSet(TestDataHelper.GetFakePayTypeList());
                mockDbContext.Setup(x => x.EventsTypes).ReturnsDbSet(TestDataHelper.GetFakeEventTypeList());

                return mockDbContext.Object;
            });

            services.AddScoped(x =>
            {
                var mockMainContext = new Mock<MainContext>();
                mockMainContext.Setup(x => x.Places).ReturnsDbSet(new List<Places>() { new Places() { Id = 1, CardCount = 0, TicketCount = 0 } });

                return mockMainContext.Object;
            });

            services.AddScoped(x =>
            {
                var db = x.GetRequiredService<CSParkingContext>();
                var configuration = x.GetRequiredService<IConfiguration>();

                var userDataAccess = new UsersDataAccess(db, configuration, new Mock<Microsoft.Extensions.Logging.ILogger<UsersDataAccess>>().Object) ;

                return userDataAccess;
            });
            
            services.AddScoped(x =>
            {
                var db = x.GetRequiredService<CSParkingContext>();
                var configuration = x.GetRequiredService<IConfiguration>();

                var cardEventsDataAccess = new CardEventsDataAccess(db, configuration, new Mock<Microsoft.Extensions.Logging.ILogger<CardEventsDataAccess>>().Object);

                return cardEventsDataAccess;
            });

            services.AddScoped(x =>
            {
                var db = x.GetRequiredService<CSParkingContext>();
                var configuration = x.GetRequiredService<IConfiguration>();

                var qrEventsDataAccess = new QrEventsDataAccess(db, configuration, new Mock<Microsoft.Extensions.Logging.ILogger<QrEventsDataAccess>>().Object);

                return qrEventsDataAccess;
            });

            services.AddScoped(x =>
            {
                var db = x.GetRequiredService<CSParkingContext>();
                var configuration = x.GetRequiredService<IConfiguration>();

                var payTypesDataAccess = new PayTypesDataAccess(db, configuration, new Mock<Microsoft.Extensions.Logging.ILogger<PayTypesDataAccess>>().Object);

                return payTypesDataAccess;
            });

            services.AddScoped(x =>
            {
                var db = x.GetRequiredService<CSParkingContext>();
                var configuration = x.GetRequiredService<IConfiguration>();

                var payTypesDataAccess = new EventTypesDataAccess(db, configuration, new Mock<Microsoft.Extensions.Logging.ILogger<EventTypesDataAccess>>().Object);

                return payTypesDataAccess;
            });

            services.AddScoped(x =>
            {
                return new CountDataAccess(x);
            });

            services.AddScoped(x => new Mock<Microsoft.Extensions.Logging.ILogger<QrCheckAlgorithm>>().Object);

            services.AddScoped<CardCheckAlgorithm>();
            services.AddScoped<QrCheckAlgorithm>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
