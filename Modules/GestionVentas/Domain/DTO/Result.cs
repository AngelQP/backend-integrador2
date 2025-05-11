using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO
{
    public class Result /*: BaseResponse*/
    {
        public Result()
        {
            this.Meta = new MetaResponse();
        }

        public bool Success { get; set; } = false;
        public MetaResponse Meta { get; set; }
        //public dynamic Data { get; set; }
    }
}
