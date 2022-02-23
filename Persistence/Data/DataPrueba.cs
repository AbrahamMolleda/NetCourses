using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class DataPrueba
    {
        public static async Task InsertarDataAsync(CursosOnlineContext context, UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    NombreCompleto = "Abraham Molleda",
                    UserName = "AbrAz0501",
                    Email = "aamol.riv@gmail.com"
                };
                await userManager.CreateAsync(usuario, "Password123$");
            }
        }
    }
}
