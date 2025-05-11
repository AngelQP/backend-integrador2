using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Contract
{
    public class QueryPagination<TResult, TFilter> : IQuery<TResult>
    {
        public QueryPagination(int startAt, int maxResult, TFilter filter)
        {
            StartAt = startAt;
            MaxResult = maxResult;
            Filter = filter;
        }

        public QueryPagination(int startAt, int? maxResult, TFilter filter)
            : this(startAt, maxResult.HasValue ? maxResult.Value : 20, filter)
        {

        }

        public QueryPagination(int startAt, TFilter filter)
            : this(startAt, 20, filter)
        {

        }

        public QueryPagination(TFilter filter) : this(0, 20, filter)
        {

        }

        public int StartAt { get; }
        public int MaxResult { get; }
        public TFilter Filter { get; }
    }
}
