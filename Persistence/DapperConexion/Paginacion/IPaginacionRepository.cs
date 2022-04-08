using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DapperConexion.Paginacion
{
    public interface IPaginacionRepository
    {
        Task<PaginacionModel> DevolverPaginacion(
            string storedProcedure, 
            int numeroPagina, 
            int cantidadElementos, 
            IDictionary<string, object> parametrosFiltro,
            string ordenamientoColumna);
    }
}
