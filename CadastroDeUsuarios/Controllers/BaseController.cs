using System;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeUsuarios.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
     
        protected IActionResult Response_BadRequest(string msgErro)
        {
            return BadRequest(new
            {
                mensagem = msgErro
            });
        }

        protected IActionResult Response_BadRequest(string msgErro, object result = null)
        {
            return BadRequest(new
            {
                success = false,
                mensagem = msgErro,
                data = result
            });
        }

     

    
    }
}
    

