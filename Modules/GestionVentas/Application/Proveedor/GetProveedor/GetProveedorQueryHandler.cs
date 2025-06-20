﻿using Bigstick.BuildingBlocks.Application.Data;
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

namespace Ferreteria.Modules.GestionVentas.Application.Proveedor.GetProveedor
{
    public class GetProveedorQueryHandler : IQueryHandler<QueryPagination<ProveedorGetDTO, GetProveedorFilters>, ProveedorGetDTO>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
    public GetProveedorQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<ProveedorGetDTO> Handle(QueryPagination<ProveedorGetDTO, GetProveedorFilters> request, CancellationToken cancellationToken)
    {
        return await GetProveedorAsync(request, cancellationToken);
    }

    public async Task<ProveedorGetDTO> GetProveedorAsync(QueryPagination<ProveedorGetDTO, GetProveedorFilters> request, CancellationToken cancellationToken)
    {
            using (var _connection = _connectionFactory.CreateNewConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", request.Filter.Nombre);
                parameters.Add("@Ruc", request.Filter.Ruc);
                parameters.Add("@Correo", request.Filter.Correo);
                parameters.Add("@contacto", request.Filter.Contacto);

                // Ejecutar la consulta y obtener tanto los Proveedores como el total
                var multi = await _connection.QueryMultipleAsync(
                "usp_ObtenerProveedores",
                parameters,
                commandType: CommandType.StoredProcedure
                );

                // Obtener los Proveedores
                var query = multi.Read<ProveedorGetDTO.ProveedorItem>();

                // Obtener el total de registros
                var total = multi.Read<int>().FirstOrDefault();

                return new ProveedorGetDTO
                {
                    Items = query,
                    Total = total
                };
            }
        }
    }
}
