using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.HttpClient.OData
{
    public class Filter
    {
        public string Attribute { get; set; }
        public FilterOperator Operator { get; set; }
        public string Value { get; set; }
    }
}
