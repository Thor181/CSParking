using CSParking.Models.Database;
using CSParking.Models.Database.MfRA;
using CSParking.Models.Web;
using CSParking.Services.DataAccess;
using CSParking.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.Algorithms.Qr
{
    public class QrCheckAlgorithm
    {
        private readonly QrEventsDataAccess _qrEventsDataAccess;
        private readonly PayTypesDataAccess _payTypesDataAccess;
        private readonly EventTypesDataAccess _eventTypesDataAccess;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public QrCheckAlgorithm(QrEventsDataAccess qrEventsDataAccess, PayTypesDataAccess payTypesDataAccess, EventTypesDataAccess eventTypesDataAccess,
            IConfiguration configuration, ILogger<QrCheckAlgorithm> logger)
        {
            _qrEventsDataAccess = qrEventsDataAccess;
            _payTypesDataAccess = payTypesDataAccess;
            _eventTypesDataAccess = eventTypesDataAccess;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GetResponseAsync(CheckQrEventDto checkQrEventDto)
        {
            var closeResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            var openResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);
            var time = _configuration.GetTimeSectionValue();

            var outEventType = _configuration.GetEventTypeName(Utils.Extensions.ConfigurationExtensions.EventType.Out);
            var passTypeOutId = await _eventTypesDataAccess.GetEventTypeIdAsync(outEventType);

            var emptyPayTypeId = await _payTypesDataAccess.GetEmptyPayTypeId();

            var cashPayTypeName = _configuration.GetPayTypeName(Utils.Extensions.ConfigurationExtensions.PayType.Cash);
            var cashPayTypeId = await _payTypesDataAccess.GetPayTypeByName(cashPayTypeName);

            var cashlessPayTypeName = _configuration.GetPayTypeName(Utils.Extensions.ConfigurationExtensions.PayType.Cashless);
            var cashlessPayTypeId = await _payTypesDataAccess.GetPayTypeByName(cashlessPayTypeName);

            var qrEventCashless = await _qrEventsDataAccess.GetQrEventAsync(checkQrEventDto.Fn, cashlessPayTypeId);

            if (qrEventCashless != null)
                return closeResponse;

            var qrEventCash = await _qrEventsDataAccess.GetQrEventAsync(checkQrEventDto.Fn, cashPayTypeId);

            if (qrEventCash == null)
            {
                var qrEventEmptyPay = await _qrEventsDataAccess.GetQrEventAsync(checkQrEventDto.Fn, emptyPayTypeId);

                if (qrEventEmptyPay == null)
                    return closeResponse;

                var subtractMinutes = (DateTime.Now - qrEventEmptyPay.Dt).Minutes;
                if (subtractMinutes > time)
                    return closeResponse;
            }

            var qrEvent = new QrEvent()
            {
                Dt = DateTime.Now,
                TypeId = passTypeOutId,
                PointId = checkQrEventDto.Point,
                PayId = cashlessPayTypeId,
                Sum = 0,
                FN = checkQrEventDto.Fn,
                FP = "-"
            };

            await _qrEventsDataAccess.WriteAsync(qrEvent);

            return openResponse;
        }

        public async Task<string> GetFnReadWsResponse(string fn)
        {
            var closeResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Close);
            var openResponse = _configuration.GetResponse(Utils.Extensions.ConfigurationExtensions.Response.Open);
            var time = _configuration.GetTimeSectionValue();

            var cashPayTypeId = await _payTypesDataAccess.GetCashPayTypeId();
            var cashlessPayTypeId = await _payTypesDataAccess.GetCashlessPayTypeId();
            var zeroPayTypeId = await _payTypesDataAccess.GetZeroPayTypeId();


            var qrEventCashless = await _qrEventsDataAccess.GetQrEventAsync(fn, cashlessPayTypeId);

            if (qrEventCashless != null)
                return closeResponse;

            var qrEventCash = await _qrEventsDataAccess.GetQrEventAsync(fn, cashPayTypeId);

            if (qrEventCash != null ) 
                return openResponse;

            var qrEventZero = await _qrEventsDataAccess.GetQrEventAsync(fn, zeroPayTypeId);

            if (qrEventZero == null)
                return closeResponse;

            var subtractMinutes = (DateTime.Now - qrEventZero.Dt).Minutes;
            if (subtractMinutes > time)
                return closeResponse;

            return openResponse;
        }
    }
}
