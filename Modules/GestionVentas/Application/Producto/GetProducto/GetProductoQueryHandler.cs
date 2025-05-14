using Bigstick.BuildingBlocks.Application.Data;
using Dapper;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Query;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Microsoft.Azure.Amqp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ferreteria.Modules.GestionVentas.Application.Contract;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.GetProducto
{
    public class GetProductoQueryHandler : IQueryHandler<QueryPagination<GetProductoDTO, GetProductoFilters>, GetProductoDTO>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
    public GetProductoQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<GetProductoDTO> Handle(QueryPagination<GetProductoDTO, GetProductoFilters> request, CancellationToken cancellationToken)
    {
        return await GetProductoAsync(request, cancellationToken);
    }

    private async Task<GetProductoDTO> GetProductoAsync(QueryPagination<GetProductoDTO, GetProductoFilters> request, CancellationToken cancellationToken)
    {
            using (var _connection = _connectionFactory.CreateNewConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", request.Filter.Nombre);
                parameters.Add("@Categoria", request.Filter.Categoria);
                parameters.Add("@Proveedor", request.Filter.Proveedor);

                // Ejecutar la consulta y obtener tanto los productos como el total
                var multi = await _connection.QueryMultipleAsync(
                    "usp_ObtenerProductos",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Obtener los productos
                var query = multi.Read<GetProductoDTO.ProductoItem>();

                // Obtener el total de registros
                var total = multi.Read<int>().FirstOrDefault();

                return new GetProductoDTO
                {
                    Items = query,
                    Total = total
                };
            }
        }
    }
}



