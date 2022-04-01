using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DapperConexion.Instructor
{
    public interface IInstructor
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();

        Task<InstructorModel> ObtenerPorId(Guid id);

        Task<int> Nuevo(string nombre, string apellido, string titulo);

        Task<int> Actualiza(InstructorModel instructor);

        Task<int> Elimina(Guid id);
    }
}
