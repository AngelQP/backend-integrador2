using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Domain.ReaderCsv
{
    public class ItemSequenceField
    {
        public ItemFieldInfo Field { get; set; }

        public string HeaderName { get; set; }

        public int Index { get; set; }
    }
}
