using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DapperConexion.Instructor
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();

        Task<InstructorModel> ObtenerPorId(Guid id);

        Task<int> Nuevo(string nombre, string apellido, string titulo);

        Task<int> Actualiza(Guid instructorId, string nombre, string apellido, string titulo);

        Task<int> Elimina(Guid id);
    }
}
