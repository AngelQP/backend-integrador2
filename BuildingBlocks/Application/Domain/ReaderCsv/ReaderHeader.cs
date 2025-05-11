using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Application.Domain.ReaderCsv
{
    public class ReaderHeader
    {
        private readonly StreamReader streamReader;

        internal readonly IEnumerable<ItemFieldInfo> fields = null;

        public List<ItemErrorFile> FileErrors { get; set; }

        private string separator;

        public ReaderHeader(StreamReader streamReader, IEnumerable<ItemFieldInfo> fields, string separator = ";")
        {
            this.streamReader = streamReader;

            this.fields = fields;

            FileErrors = new List<ItemErrorFile>();

            this.separator = separator;
        }

        private void AddErrorsHeadersDuplication(string[] headersNames)
        {
            var headerDuplicate = headersNames.GroupBy(x => x.ToLower().Trim()).Where(y => y.Count() > 1 && !string.IsNullOrWhiteSpace(y.Key));
            foreach (var header in headerDuplicate)
            {
                FileErrors.Add(new ItemErrorFile()
                {
                    ID = Guid.NewGuid(),
                    Field = header.Key,
                    ErrorType = ErrorTypeFile.Duplicated
                });
            }
        }
        private void AddErrorsHeadersUnknow(string[] headersNames)
        {
            var unknowHeader = from header in headersNames
                               join field in fields on header.ToLower().Trim() equals field.Name.ToLower().Trim() into leftRigth
                               from leftRight in leftRigth.DefaultIfEmpty()
                               where leftRight == null
                               select header;
            foreach (var header in unknowHeader)
            {
                FileErrors.Add(new ItemErrorFile()
                {
                    ID = Guid.NewGuid(),
                    Field = header,
                    ErrorType = ErrorTypeFile.Unknown
                });
            }

        }
        private void AddErrorsHeadersNotFound(string[] headersNames)
        {
            var notFoundFields = from field in fields
                                 join header in headersNames on field.Name.ToLower().Trim() equals header.ToLower().Trim() into leftRigth
                                 from leftRight in leftRigth.DefaultIfEmpty()
                                 where leftRight == null
                                 select field;
            foreach (var field in notFoundFields)
            {
                FileErrors.Add(new ItemErrorFile()
                {
                    ID = Guid.NewGuid(),
                    Field = field.Name,
                    ErrorType = ErrorTypeFile.NotFound
                });
            }

        }

        public IEnumerable<ItemSequenceField> ReadHeader()
        {
            if (streamReader.Peek() > -1)
            {
                var headerLine = streamReader.ReadLine();

                var headersNames = headerLine.Split(separator);

                AddErrorsHeadersDuplication(headersNames);

                AddErrorsHeadersUnknow(headersNames);

                AddErrorsHeadersNotFound(headersNames);

                return (from header in headersNames
                        join field in fields on header.ToLower().Trim() equals field.Name.ToLower().Trim()
                        select new ItemSequenceField() { HeaderName = header, Field = field, Index = Array.IndexOf(headersNames, header) }).ToList();
            }
            return null;

        }


    }
}
