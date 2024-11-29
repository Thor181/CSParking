using CSParking.Models.Database.CsParking.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Services.Initialization
{
    public static class DatabaseInitialization
    {
        public static void Initialize(DbContext dbContext)
        {
            dbContext.Database.Migrate();
        }
    }
}
