using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using Dapper;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Cliente;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Proveedor;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Domain.GestionVentas
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly IEntityHub entityHub;
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly ISqlTransaction _sqlTransaction;

        public ProveedorRepository(IEntityHub entityHub, ISqlConnectionFactory sqlConnectionFactory, ISqlTransaction sqlTransaction)
        {
            this.entityHub = entityHub;
            this.sqlConnectionFactory = sqlConnectionFactory;
            _sqlTransaction = sqlTransaction;
        }

        public async Task<int> CrearProveedor(CrearProveedorRequest request)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();

                parameters.Add("@nombre", request.Nombre);
                parameters.Add("@ruc", request.Ruc);
                parameters.Add("@direccion", request.Direccion);
                parameters.Add("@telefono", request.Telefono);
                parameters.Add("@correo", request.Correo);
                parameters.Add("@contacto", request.Contacto);
                parameters.Add("@estado", request.Estado);
                parameters.Add("@fecha_registro", request.Fecha_Registro);

                return await connection.ExecuteAsync(
                    "[dbo].[sp_CrearProveedor]",
                    parameters,
                    transaction,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<(IEnumerable<UserDTO>, int)> UsersGet(string nombre, string contacto, int startAt, int maxResult)
        {
            using (var _connection = sqlConnectionFactory.CreateNewConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", nombre);
                parameters.Add("@contacto", contacto);
                //--
                parameters.Add("@startAt", startAt);
                parameters.Add("@maxResult", maxResult);

                parameters.Add("@total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var query = await _connection.QueryAsync<UserDTO>("[dbo].[sp_ObtenerProveedor]", parameters, commandType: CommandType.StoredProcedure);
                var total = parameters.Get<int>("@total");

                return (query, total);
            }
        }
    }
}
