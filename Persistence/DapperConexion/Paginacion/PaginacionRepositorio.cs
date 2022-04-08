using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.DapperConexion.Paginacion
{
    public class PaginacionRepositorio : IPaginacionRepository
    {
        private readonly IFactoryConnection _factoryConnection;
        public PaginacionRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }
        public async Task<PaginacionModel> DevolverPaginacion(string storedProcedure, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamientoColumna)
        {
            PaginacionModel paginacionModel = new PaginacionModel();
            List<IDictionary<string, object>> listaReporte = null;
            int totalRecords = 0;
            int totalPaginas = 0;
            try
            {
                var connection = _factoryConnection.GetConnection();
                DynamicParameters parameters = new DynamicParameters();
                foreach(var param in parametrosFiltro)
                {
                    parameters.Add("@" + param.Key, param.Value);
                }
                parameters.Add("@NumeroPagina", numeroPagina);
                parameters.Add("@CantidadElementos", cantidadElementos);
                parameters.Add("@Ordenamiento", ordenamientoColumna);
                parameters.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);

                var result = await connection.QueryAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginacionModel.ListaRecords = listaReporte;
                paginacionModel.NumeroPaginas = parameters.Get<int>("@TotalPaginas");
                paginacionModel.TotalRecords = parameters.Get<int>("@TotalRecords");
            }catch(Exception e)
            {
                throw new Exception("No se puede ejcutar el procedimiento almacenado", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return paginacionModel;
        }
    }
}
