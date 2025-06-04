using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.Infrastructure;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using Dapper;
using Ferreteria.Modules.GestionVentas.Application.Producto.GetCategorias;
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

                parameters.Add("@Nombre", request.Nombre);
                parameters.Add("@Descripcion", request.Descripcion);
                parameters.Add("@Sku", request.Sku);
                parameters.Add("@Marca", request.Marca);
                parameters.Add("@Modelo", request.Modelo);
                parameters.Add("@UnidadMedida", request.Unidad); 
                parameters.Add("@Categoria", request.Categoria); 
                parameters.Add("@Subcategoria", request.Subcategoria);
                parameters.Add("@ImpuestoTipo", request.ImpuestoTipo);
                parameters.Add("@PrecioUnitario", request.Precio);
                parameters.Add("@Stock", request.Cantidad); 
                parameters.Add("@Costo", request.Costo);
                parameters.Add("@Proveedor", request.Proveedor);
                parameters.Add("@CodigoBarra", request.CodigoBarras);
                parameters.Add("@UsuarioCreacion", request.UsuarioCreacion);

                return await connection.ExecuteAsync(
                    "[fer].[usp_CrearProducto]",
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

                var query = await _connection.QueryAsync<ProductoDTO>("[fer].[usp_ObtenerProductos]", parameters, commandType: CommandType.StoredProcedure);
                var total = parameters.Get<int>("@total");

                return (query, total);
            }
        }
        public async Task<IEnumerable<CategoriasLiteDTO>> GetCategoriaLite()
        {
            using var connection = sqlConnectionFactory.CreateNewConnection();

            const string storedProcedure = "[fer].[usp_ObtenerCategoriasLite]";

            var result = await connection.QueryAsync<CategoriasLiteDTO>(
                storedProcedure,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}
