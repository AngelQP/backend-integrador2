using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Domain.Model
{
    public class BaseRequest
    {
        public BaseRequest()
        {
            this.Meta = new MetaRequest();
        }

        public MetaRequest Meta { get; set; }
    }
}
