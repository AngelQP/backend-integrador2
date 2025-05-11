using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Domain.ReaderCsv
{
    public abstract class ReaderBody<T>
    {
        protected readonly IEnumerable<ItemSequenceField> itemsSequenceFields;

        private readonly StreamReader streamReader;
        string separator;
        public ReaderBody(StreamReader streamReader, IEnumerable<ItemSequenceField> itemsSequenceFields, string separator = ";")
        {
            this.itemsSequenceFields = itemsSequenceFields;
            this.streamReader = streamReader;
            this.separator = separator;
        }

        public IEnumerable<T> Resolve()
        {
            var x = 1;
            while (streamReader.Peek() > -1)
            {
                var line = streamReader.ReadLine();

                if (!string.IsNullOrWhiteSpace(line) && line.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length > 0)
                {
                    var recordItem = line.Split(separator);

                    yield return CreateItemExistence(recordItem, x);
                }

                x++;
            }
        }

        protected abstract T CreateItemExistence(string[] recordItem, int rowIndex);
    }

}
