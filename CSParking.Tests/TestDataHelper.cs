using CSParking.Models.Database;
using CSParking.Models.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Tests
{
    public class TestDataHelper
    {
        public static List<User> GetFakeUserList()
        {
            var list = new List<User>();
            list.Add(new User
            {
                Before = DateTime.Parse("01.01.1999"),
                Card = "Card1",
                Id = Guid.NewGuid(),
                PlaceId = 1,
                Staff = false
            });

            list.Add(new User
            {
                Before = DateTime.Parse("01.01.2999"),
                Card = "Card2",
                Id = Guid.NewGuid(),
                PlaceId = 1,
                Staff = false
            });

            list.Add(new User
            {
                Before = DateTime.Parse("01.01.2999"),
                Card = "Card3",
                Id = Guid.NewGuid(),
                PlaceId = 1,
                Staff = false
            });

            list.Add(new User
            {
                Before = DateTime.Parse("01.01.2999"),
                Card = "Card4",
                Id = Guid.NewGuid(),
                PlaceId = 1,
                Staff = true
            });

            return list;
        }

        public static List<QrEvent> GetFakeQrEventList()
        {
            var list = new List<QrEvent>();

            list.Add(new QrEvent()
            {
                Dt = DateTime.Now,
                FN = "1234567",
                FP = "9876543",
                Id = Guid.NewGuid(),
                PayId = 2,
                PointId = 1,
                Sum = 300,
                TypeId = 1
            }) ;

            list.Add(new QrEvent()
            {
                Dt = DateTime.Now,
                FN = "2234567",
                FP = "9876543",
                Id = Guid.NewGuid(),
                PayId = 1,
                PointId = 1,
                Sum = 300,
                TypeId = 1
            });

            list.Add(new QrEvent()
            {
                Dt = DateTime.Now.AddMinutes(-7),
                FN = "3234567",
                FP = "9876543",
                Id = Guid.NewGuid(),
                PayId = 0,
                PointId = 1,
                Sum = 300,
                TypeId = 1
            });

            list.Add(new QrEvent()
            {
                Dt = DateTime.Now.AddMinutes(-77),
                FN = "9994567",
                FP = "9876543",
                Id = Guid.NewGuid(),
                PayId = 0,
                PointId = 1,
                Sum = 300,
                TypeId = 1
            });

            return list;
        }

        public static List<PayType> GetFakePayTypeList()
        {
            var list = new List<PayType>();

            list.Add(new PayType()
            {
                Id = 1,
                Name = "Нал",
            });

            list.Add(new PayType()
            {
                Id = 2,
                Name = "Безнал",
            });

            list.Add(new PayType()
            {
                Id = 0,
                Name = "-",
            });

            return list;
        }

        public static List<EventType> GetFakeEventTypeList()
        {
            var list = new List<EventType>();

            list.Add(new EventType()
            {
                Id = 1,
                Name = "Вход"
            });

            list.Add(new EventType()
            {
                Id = 2,
                Name = "Выход"
            });

            return list;
        }
    }
}
