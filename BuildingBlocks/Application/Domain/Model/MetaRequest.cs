using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Domain.Model
{
    public class MetaRequest
    {
        public string Modifier { get; set; }
        public int CurrentPage { get; set; }
        public int Size { get; set; }
        public Guid Identifier { get; set; }
    }
}
