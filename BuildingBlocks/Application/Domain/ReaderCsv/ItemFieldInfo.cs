using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Domain.ReaderCsv
{
    public class ItemFieldInfo
    {
        public string Name { get; set; }
        public string PropertyName { get; set; }
        public string Format { get; set; }
        public Type DataType { get; set; }
        public bool IsMandatory { get; set; }
    }
}
