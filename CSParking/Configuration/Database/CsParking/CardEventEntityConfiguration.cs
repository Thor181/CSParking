using CSParking.Configuration.Database.Interfaces;
using CSParking.Models.Database.MfRA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Configuration.Database.CsParking
{
    public class CardEventEntityConfiguration : IEntityTypeConfiguration<CardEvent>, ICsParkingEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<CardEvent> builder)
        {
        }
    }
}
