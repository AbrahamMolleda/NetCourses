using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contratos
{
    public interface IJwtGenerador
    {
        string CrearToken(Usuario usuario);
    }
}
