using CSParking.Models.Database.Base;
using CSParking.Models.Database.Context;
using CSParking.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.DataAccess
{
    public class BaseDataAccess
    {
        protected CSParkingContext DbContext { get; init; }
        protected IConfiguration Configuration { get; set; }
        protected ILogger Logger { get; set; }

        public BaseDataAccess(CSParkingContext db, IConfiguration configuration, ILogger<BaseDataAccess> logger)
        {
            DbContext = db;
            Configuration = configuration;
            Logger = logger;
        }

        protected async ValueTask WriteAsync<T>(T data, int? retryCount = null, int? retryDelayMilliseconds = null) where T : class, IDatabaseEntity
        {
            var retryConfiguration = Configuration.GetRetryConfiguration();

            retryCount ??= retryConfiguration.RetryCount;
            retryDelayMilliseconds ??= retryConfiguration.RetryDelayMilliseconds;

            var retryIteration = 0;
            while (retryIteration < retryCount)
            {
                try
                {
                    await DbContext.Set<T>().AddAsync(data);
                    await DbContext.SaveChangesAsync();
                    break;
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error write to database (Retry: {retryIteration}/{retryCount})", retryIteration + 1, retryCount);
                }

                retryIteration++;
                await Task.Delay((int)retryDelayMilliseconds);
            }
        }
    }
}
