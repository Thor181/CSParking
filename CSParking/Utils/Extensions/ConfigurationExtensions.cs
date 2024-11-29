using CSParking.Models.Configuration.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Utils.Extensions
{
    public static class ConfigurationExtensions
    {
        public enum Response
        {
            DatabaseNotAvailable,
            Close,
            Open
        }

        public enum PayType
        {
            EmptyPayTypeValue, 
            Cash,
            Cashless,
            Zero
        }

        public enum EventType
        {
            In,
            Out
        }

        public static int GetTimeSectionValue(this IConfiguration configuration)
        {
            var sectionValue = configuration.GetRequiredSection("Time").Value;

            var isParsed = int.TryParse(sectionValue, out int value);

            if (!isParsed)
                return -1;

            return value;
        }

        public static string GetResponse(this IConfiguration configuration, Response response)
        {
            var responses = configuration.GetRequiredSection("Responses");
            return responses.GetRequiredSection(Enum.GetName(response)!).Value;
        }

        public static RetryConfiguration GetRetryConfiguration(this IConfiguration configuration)
        {
            var retryConfiguration = new RetryConfiguration();

            var rootSection = configuration.GetRequiredSection("Retry");

            var countRaw = rootSection.GetRequiredSection("RetryCount").Value;
            var countIsParsed = int.TryParse(countRaw, out int count);
            if (!countIsParsed)
                count = 1;

            var delayRaw = rootSection.GetRequiredSection("RetryDelayMilliseconds").Value;
            var delayIsParsed = int.TryParse(delayRaw, out int delay);
            if (delayIsParsed)
                delay = 1000;

            retryConfiguration.RetryCount = count;
            retryConfiguration.RetryDelayMilliseconds = delay;

            return retryConfiguration;
        }

        public static string GetPayTypeName(this IConfiguration configuration, PayType payType)
        {
            var payTypes = configuration.GetRequiredSection("PayTypes");
            return payTypes.GetRequiredSection(Enum.GetName(payType)!).Value;
        }

        public static string GetEventTypeName(this IConfiguration configuration,  EventType eventType)
        {
            var type = configuration.GetRequiredSection("EventTypes");
            return type.GetRequiredSection(Enum.GetName(eventType)!).Value;
        }

        public static bool GetNeedCloseResponseIfLessThanZero(this IConfiguration configuration)
        {
            var value = Convert.ToBoolean(configuration.GetSection("Count:CloseResponseIfLessThanZero").Value);

            return value;
        }
    }
}
