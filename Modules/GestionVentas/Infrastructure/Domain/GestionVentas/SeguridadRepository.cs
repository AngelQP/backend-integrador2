using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.Extensions;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using Dapper;
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
    public class SeguridadRepository : ISeguridadRepository
    {
        private readonly IEntityHub entityHub;
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly ISqlTransaction _sqlTransaction;

        public SeguridadRepository(IEntityHub entityHub, ISqlConnectionFactory sqlConnectionFactory, ISqlTransaction sqlTransaction)
        {
            this.entityHub = entityHub;
            this.sqlConnectionFactory = sqlConnectionFactory;
            _sqlTransaction = sqlTransaction;
        }

        public async Task<int> CrearUsuario(CrearUsuarioRequest request)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();
                parameters.Add("@sociedad", request.Sociedad);
                parameters.Add("@usuario", request.Usuario);
                parameters.Add("@correo", request.Correo);
                parameters.Add("@nombre", request.Nombre);
                parameters.Add("@apellidoPaterno", request.ApellidoPaterno);
                parameters.Add("@apellidoMaterno", request.ApellidoMaterno);
                parameters.Add("@telefono", request.Telefono);
                parameters.Add("@contrasenia", request.Contrasenia);
                parameters.Add("@rol", request.Rol);
                parameters.Add("@usuarioCreacion", request.UsuarioCreacion);

                return await connection.ExecuteAsync("[fer].[usp_CrearUsuario]", parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<string> ValidarUsuarioOCorreo(string usuario, string correo)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();
                parameters.Add("@usuario", usuario);
                parameters.Add("@correo", correo);

                var resultado = await connection.QuerySingleAsync<string>("[fer].[usp_ValidarUsuarioOCorreo]", parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                return resultado;
            }
        }

        public async Task<UsuarioDTO> ObtenerUsuarioAsync(string usuario)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;

                var parameters = new DynamicParameters();
                parameters.Add("@usuario", usuario);

                return await connection.QueryFirstOrDefaultAsync<UsuarioDTO>("[fer].[usp_ObtenerUsuarioPorNombreOCorreo]", parameters, transaction, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> GuardarOTP(GuardarOTPRequest request)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();
                parameters.Add("@idUsuario", request.IdUsuario);
                parameters.Add("@correo", request.Correo);
                parameters.Add("@codigo", request.Codigo);
                parameters.Add("@expiracion", request.Expiracion);
                parameters.Add("@usuario", request.Usuario);

                return await connection.ExecuteAsync("[fer].[usp_CrearCodigoVerificacion]", parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<ObtenerCodigoVerificacionDTO> ObtenerCodigoVerificacion(string correo)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;

                var parameters = new DynamicParameters();
                parameters.Add("@correo", correo);

                return await connection.QueryFirstOrDefaultAsync<ObtenerCodigoVerificacionDTO>("[fer].[usp_ObtenerCodigoVerificacion]", parameters, transaction, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> ActualizarContraseniaUsuario(int id, string contrasenia, string usuario)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                parameters.Add("@contrasenia", contrasenia);
                parameters.Add("@usuario", usuario);

                return await connection.ExecuteAsync("[fer].[usp_ActualizarContraseniaUsuario]", parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> ActualizarCodigoVerificacion(int id, string usuario)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                parameters.Add("@usuario", usuario);

                return await connection.ExecuteAsync("[fer].[usp_ActualizarCodigoVerificacion]", parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<(IEnumerable<UserDTO>, int)> UsersGet(string nombre, int startAt, int maxResult)
        {
            using (var _connection = sqlConnectionFactory.CreateNewConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", nombre);
                //--
                parameters.Add("@startAt", startAt);
                parameters.Add("@maxResult", maxResult);

                parameters.Add("@total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var query = await _connection.QueryAsync<UserDTO>("[fer].[usp_ObtenerUsuarios]", parameters, commandType: CommandType.StoredProcedure);
                var total = parameters.Get<int>("@total");

                return (query, total);
            }
        }

        public async Task<UserDTO> UserGetById(int idUsuario)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;

                var parameters = new DynamicParameters();
                parameters.Add("@idUsuario", idUsuario);

                return await connection.QueryFirstOrDefaultAsync<UserDTO>("[fer].[usp_ObtenerUsuarioPorId]", parameters, transaction, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
