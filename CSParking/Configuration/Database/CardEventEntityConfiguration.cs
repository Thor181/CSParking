using CSParking.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Configuration.Database
{
    public class CardEventEntityConfiguration : IEntityTypeConfiguration<CardEvent>
    {
        public void Configure(EntityTypeBuilder<CardEvent> builder)
        {
        }
    }
}
