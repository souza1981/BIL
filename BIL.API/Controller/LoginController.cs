using BIL.Seguranca;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIL_API.Controller
{
    [ApiController]
    [Route("[controller]")]

    public class LoginController
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody] AccessCredentials credenciais,
            [FromServices] AccessManager accessManager) 
        {
            if (accessManager.ValidateCredentials(credenciais))
            {
                return accessManager.GenerateToken(credenciais);
            }
            else
            {
                return new
                {
                    Authenticated = false,
                    Message = "Falha ao autenticar"
                };
            }
        }
    }
}
