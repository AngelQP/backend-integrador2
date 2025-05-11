using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Domain.ReaderCsv
{
    public class ItemErrorFile
    {
        public Guid ID { get; set; }
        public string Field { get; set; }
        public ErrorTypeFile ErrorType { get; set; }
    }

    public enum ErrorTypeFile 
    {
        Duplicated = 0,
        NotFound = 1,
        Unknown = 2
    }
}
