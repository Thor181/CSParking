using CSParking.Models.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Models.Database
{
    public class User : IDatabaseEntity
    {
        public Guid Id { get; set; }
        public string? Card { get; set; }
        public DateTime? Before { get; set; }
        public int? PlaceId { get; set; }
        public bool? Staff { get; set; }
    }
}
