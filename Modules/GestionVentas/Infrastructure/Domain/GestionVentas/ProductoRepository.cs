using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.Infrastructure;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using Dapper;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Infrastructure.Domain.GestionVentas
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IEntityHub entityHub;
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly ISqlTransaction _sqlTransaction;

        public ProductoRepository(IEntityHub entityHub, ISqlConnectionFactory sqlConnectionFactory, ISqlTransaction sqlTransaction)
        {
            this.entityHub = entityHub;
            this.sqlConnectionFactory = sqlConnectionFactory;
            _sqlTransaction = sqlTransaction;
        }

        public async Task<int> CrearProducto(CrearProductoRequest request)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();

                parameters.Add("@nombre", request.Nombre);
                parameters.Add("@descripcion", request.Descripcion);
                parameters.Add("@sku", request.Sku);
                parameters.Add("@marca", request.Marca);
                parameters.Add("@modelo", request.Modelo);
                parameters.Add("@unidad", request.Unidad);
                parameters.Add("@categoria", request.Categoria);
                parameters.Add("@subcategoria", request.Subcategoria);
                parameters.Add("@impuestoTipo", request.ImpuestoTipo);
                parameters.Add("@precio", request.Precio);
                parameters.Add("@cantidad", request.Cantidad);
                parameters.Add("@costo", request.Costo);
                parameters.Add("@proveedor", request.Proveedor);
                parameters.Add("@codigoBarras", request.CodigoBarras);
                parameters.Add("@usuarioCreacion", request.UsuarioCreacion);
                parameters.Add("@FechaCreacion", request.FechaCreacion);

                return await connection.ExecuteAsync(
                    "[dbo].[usp_CrearProducto]",
                    parameters,
                    transaction,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task<(IEnumerable<ProductoDTO>, int)> ProductoGet(string nombre, string categoria, string proveedor, int startAt, int maxResult)
        {
            using (var _connection = sqlConnectionFactory.CreateNewConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", string.IsNullOrWhiteSpace(nombre) ? null : nombre);
                parameters.Add("@categoria", string.IsNullOrWhiteSpace(categoria) ? null : categoria);
                parameters.Add("@proveedor", string.IsNullOrWhiteSpace(proveedor) ? null : proveedor);
                //--
                parameters.Add("@startAt", startAt);
                parameters.Add("@maxResult", maxResult);

                parameters.Add("@total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var query = await _connection.QueryAsync<ProductoDTO>("[dbo].[usp_ObtenerProductos]", parameters, commandType: CommandType.StoredProcedure);
                var total = parameters.Get<int>("@total");

                return (query, total);
            }
        }
    }
}
