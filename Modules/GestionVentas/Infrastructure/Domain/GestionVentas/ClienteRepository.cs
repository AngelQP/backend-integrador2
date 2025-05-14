using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using Dapper;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Cliente;
using Ferreteria.Modules.GestionVentas.Domain.Repository;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Domain.GestionVentas
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IEntityHub entityHub;
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly ISqlTransaction _sqlTransaction;

        public ClienteRepository(IEntityHub entityHub, ISqlConnectionFactory sqlConnectionFactory, ISqlTransaction sqlTransaction)
        {
            this.entityHub = entityHub;
            this.sqlConnectionFactory = sqlConnectionFactory;
            _sqlTransaction = sqlTransaction;
        }

        public async Task<int> CrearCliente(CrearClienteRequest request)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();

                parameters.Add("@nombre", request.Nombre);
                parameters.Add("@apellidos", request.Apellidos);
                parameters.Add("@dni", request.Dni);
                parameters.Add("@ruc", request.Ruc);
                parameters.Add("@direccion", request.Direccion);
                parameters.Add("@telefono", request.Telefono);
                parameters.Add("@correo", request.Correo);
                parameters.Add("@FechaRegistro", request.FechaRegistro);
                parameters.Add("@Esempresa", request.EsEmpresa);

                return await connection.ExecuteAsync(
                    "[dbo].[usp_CrearCliente]",
                    parameters,
                    transaction,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
