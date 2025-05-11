using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.HttpClient.OData
{
    public class PagingOptions
    {
        public PagingOptions(int startAt, int maxResult)
        {
            this.StartAt = startAt;
            this.MaxResult = maxResult;
        }

        public PagingOptions()
            : this(1, 20)
        {

        }

        public int StartAt { get; set; }

        public int MaxResult { get; set; }
    }
}
