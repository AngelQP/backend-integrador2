using Bigstick.BuildingBlocks.Application.Data;
using Bigstick.BuildingBlocks.Infrastructure;
using Bigstick.BuildingBlocks.ObjectStick.Hub;
using Dapper;
using Ferreteria.Modules.GestionVentas.Application.Producto.GetCategorias;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Producto;
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
                parameters.Add("@UnidadMedida", request.UnidadMedida); 
                parameters.Add("@Categoria", request.Categoria); 
                parameters.Add("@Subcategoria", request.Subcategoria);
                parameters.Add("@ImpuestoTipo", request.ImpuestoTipo);
                parameters.Add("@PrecioUnitario", request.PrecioUnitario);
                parameters.Add("@Stock", request.Stock); 
                parameters.Add("@Costo", request.Costo);
                parameters.Add("@Proveedor", request.Proveedor);
                parameters.Add("@CodigoBarra", request.CodigoBarra);
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
        public async Task<IEnumerable<ProveedorLiteDTO>> GetProveedorLite()
        {
            using var connection = sqlConnectionFactory.CreateNewConnection();

            const string storedProcedure = "[fer].[usp_GetProveedoresLite]";

            var result = await connection.QueryAsync<ProveedorLiteDTO>(
                storedProcedure,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<ProductoDTO> ProductGetById(int idProducto)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;

                var parameters = new DynamicParameters();
                parameters.Add("@idProducto", idProducto);

                return await connection.QueryFirstOrDefaultAsync<ProductoDTO>("[fer].[usp_ObtenerProductoPorId]", parameters, transaction, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> ActualizarProducto(ActualizarProductoRequest request)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();

                parameters.Add("@IdProducto", request.IdProducto);
                parameters.Add("@Nombre", request.Nombre);
                parameters.Add("@Descripcion", request.Descripcion);
                parameters.Add("@PrecioUnitario", request.PrecioUnitario);
                parameters.Add("@Stock", request.Stock);
                parameters.Add("@Categoria", request.Categoria);
                parameters.Add("@CodigoBarra", request.CodigoBarra);
                parameters.Add("@UnidadMedida", request.UnidadMedida);
                parameters.Add("@EstadoRegistro", request.EstadoRegistro);
                parameters.Add("@Sku", request.Sku);
                parameters.Add("@Marca", request.Marca);
                parameters.Add("@Modelo", request.Modelo);
                parameters.Add("@Subcategoria", request.Subcategoria);
                parameters.Add("@ImpuestoTipo", request.ImpuestoTipo);
                parameters.Add("@Costo", request.Costo);
                parameters.Add("@Proveedor", request.Proveedor);
                parameters.Add("@UsuarioActualizacion", request.UsuarioActualizacion);

                return await connection.ExecuteAsync(
                    "[fer].[usp_ActualizarProducto]",
                    parameters,
                    transaction,
                    commandType: System.Data.CommandType.StoredProcedure
                );
            }
        }
        public async Task<int> CrearLote(CrearLoteRequest request)
        {
            using (var connection = sqlConnectionFactory.CreateNewConnection())
            {
                var transaction = _sqlTransaction.Transaction;
                var parameters = new DynamicParameters();

                parameters.Add("@IdProducto", request.IdProducto);
                parameters.Add("@NumeroLote", request.NumeroLote);
                parameters.Add("@FechaIngreso", request.FechaIngreso);
                parameters.Add("@FechaFabricacion", request.FechaFabricacion);
                parameters.Add("@FechaVencimiento", request.FechaVencimiento);
                parameters.Add("@CantidadInicial", request.CantidadInicial);
                parameters.Add("@CantidadDisponible", request.CantidadDisponible);
                parameters.Add("@CostoUnitario", request.CostoUnitario);
                parameters.Add("@UsuarioCreacion", request.UsuarioCreacion);
                parameters.Add("@FechaCreacion", request.FechaCreacion);
                parameters.Add("@IdProveedor", request.IdProveedor);

                return await connection.ExecuteAsync(
                    "[fer].[usp_CrearLote]",
                    parameters,
                    transaction,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        public async Task<IEnumerable<UsuariosLiteDTO>> GetUsuarioLite()
        {
            using var connection = sqlConnectionFactory.CreateNewConnection();

            const string storedProcedure = "[fer].[usp_ObtenerUsuariosFiltrados]";

            var result = await connection.QueryAsync<UsuariosLiteDTO>(
                storedProcedure,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

    }
}
