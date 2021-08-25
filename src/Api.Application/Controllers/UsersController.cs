using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    //Aqui cria as rotas das api
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IUserService service){

        }
    }
}