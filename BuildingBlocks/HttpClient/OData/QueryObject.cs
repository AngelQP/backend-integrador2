using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.HttpClient.OData
{
	public class QueryObject
	{
		public QueryObject()
		{
			this.Filters = new Filter[] { };
			// this.PageOption = new PagingOptions();
			//this.OrderBys = new OrderByOptions[] { };
			this.Expands = new Expand[] { };
			this.QueryParams = new Dictionary<string, string>();
		}
		public bool Metadata { get; set; }

		/// <summary>
		/// $filter=attribute eq value 
		/// </summary>
		public Filter[] Filters { get; set; }

		/// <summary>
		/// $select=attribute,attribute,attribute
		/// </summary>
		public string Select { get; set; }

		/// <summary>
		/// $order
		/// </summary>
		public OrderByOptions[] OrderBys { get; set; }

		public PagingOptions PageOption { get; set; }

		/// <summary>

		/// <summary>
		/// $expand
		/// </summary>
		public Expand[] Expands { get; set; }

		public Dictionary<string, string> QueryParams { get; set; }

		public string ToQueryString()
		{
			return string.Empty;
		}

		public T GetValueFilter<T>(string name)
		{
			var filter = this.Filters.FirstOrDefault(x => name.Equals(x.Attribute, StringComparison.CurrentCultureIgnoreCase));
			if (filter != null)
			{
				if (typeof(DateTime) == typeof(T) || typeof(DateTime?) == typeof(T))
				{
					string[] formats = new[] { "dd/MM/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/M/yyyy" };
					if (!string.IsNullOrWhiteSpace(filter.Value))
					{
						DateTime r;
						if (DateTime.TryParseExact(filter.Value, formats, null, System.Globalization.DateTimeStyles.None, out r))
							return (T)(object)r;
						return default(T);
					}

				}
				else
				{
					TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
					return (T)typeConverter.ConvertFromString(filter.Value);
				}

			}
			return default(T);
		}
	}
}
