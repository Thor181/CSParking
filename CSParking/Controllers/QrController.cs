using CSParking.Models.Applied;
using CSParking.Models.Web;
using CSParking.Services.Algorithms.Qr;
using CSParking.Services.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Controllers
{
    [ApiController]
    [Route("/qr/[action]")]
    public class QrController : ControllerBase
    {
        private readonly QrEventsDataAccess _eventsDataAccess;
        private readonly QrCheckAlgorithm _qrCheckAlgorithm;

        public QrController(QrEventsDataAccess qrEventsDataAccess, QrCheckAlgorithm qrCheckAlgorithm)
        {
            _eventsDataAccess = qrEventsDataAccess;
            _qrCheckAlgorithm = qrCheckAlgorithm;
        }

        [HttpGet]
        public async Task WriteQrEventAsync([FromQuery] QrEventData qrEventData)
        {
            await _eventsDataAccess.WriteAsync(qrEventData);
        }

        [HttpGet]
        public async Task<string> CheckQrEventAsync([FromQuery] CheckQrEventDto checkQrEventDto)
        {
            var response = await _qrCheckAlgorithm.GetResponseAsync(checkQrEventDto);
            return response;
        }

        [HttpGet]
        public async Task<string> ReadQrEventAsync(string fn)
        {
            var qrEvent = await _eventsDataAccess.GetQrEventAsync(fn);
            var response = $"@{qrEvent?.Dt.ToString("G") ?? string.Empty}%";
            return response;
        }

        [HttpGet]
        public async Task<string> ReadQrEventMinutesAsync(string fn)
        {
            var qrEvent = await _eventsDataAccess.GetQrEventAsync(fn);
            var response = $"@{Math.Ceiling((DateTime.Now - qrEvent.Dt).TotalMinutes)}%";
            return response;
        }

        [HttpGet]
        public async Task<string> FnReadWsAsync(string fn)
        {
            var response = await _qrCheckAlgorithm.GetFnReadWsResponse(fn);
            return response;
        }
    }
}
