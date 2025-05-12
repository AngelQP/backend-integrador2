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
                parameters.Add("@usuarioCreacion", request.UsuarioCreacion);

                return await connection.ExecuteAsync("[dbo].[usp_CrearUsuario]", parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
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

                var resultado = await connection.QuerySingleAsync<string>("[dbo].[usp_ValidarUsuarioOCorreo]", parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                return resultado;
            }
        }

        public async Task<UsuarioDTO> GetUsuarioAsync(string usuario)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;

                var parameters = new DynamicParameters();
                parameters.Add("@usuario", usuario);

                return await connection.QueryFirstOrDefaultAsync<UsuarioDTO>("[dbo].[usp_ObtenerUsuarioPorNombreOCorreo]", parameters, transaction, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
