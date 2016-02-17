using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagametTypes
{
    public class Mortgage
    {
        public ObjectId id { get; private set; }
        public DateTime end_date { get; set; }
        public decimal money_amount { get; set; }
    }
}
