using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagametTypes
{
    public class Mortgage : EntityBase
    {       
        public DateTime end_date { get; set; }
        public decimal amount { get; set; }
    }
}
