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

namespace Ferreteria.Modules.GestionVentas.Application.Cliente.GetCliente
{
    public class GetClienteQueryHandler : IQueryHandler<QueryPagination<GetClienteDTO, GetClienteFilters>, GetClienteDTO>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
    public GetClienteQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<GetClienteDTO> Handle(QueryPagination<GetClienteDTO, GetClienteFilters> request, CancellationToken cancellationToken)
    {
        return await GetClienteAsync(request, cancellationToken);
    }

    private async Task<GetClienteDTO> GetClienteAsync(QueryPagination<GetClienteDTO, GetClienteFilters> request, CancellationToken cancellationToken)
    {
            using (var _connection = _connectionFactory.CreateNewConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", request.Filter.Nombre);
                parameters.Add("@Apellidos", request.Filter.Apellidos);
                parameters.Add("@Dni", request.Filter.Dni);
                parameters.Add("@Ruc", request.Filter.Ruc);

                // Ejecutar la consulta y obtener tanto los Clientes como el total
                var multi = await _connection.QueryMultipleAsync(
                    "usp_ObtenerClientes",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Obtener los Clientes
                var query = multi.Read<GetClienteDTO.ClienteItem>();

                // Obtener el total de registros
                var total = multi.Read<int>().FirstOrDefault();

                return new GetClienteDTO
                {
                    Items = query,
                    Total = total
                };
            }
        }
    }
}



