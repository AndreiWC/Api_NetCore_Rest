using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    //Aqui cria as rotas das api, equivale ao Http:localhost:5000/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase //Extende a ControllerBase do pacote Microsoft.AspNetCore.Mvc onde faz o gerenciamento das rotas
    {
        //AQUI INICIA OS METODOS DA API RESTFULL
        //METODO DO TIPO GET, RETORNA TODOS OS DADOS 
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IUserService service)
        {
            if (!ModelState.IsValid) // QUANDO MANDA UMA INFORMAÇÃO PARA ROTA ELA PREENCHE  O ModelState, AQUI É VERIFICADO SE A INFORMAÇÃO É VALIDA
            {
                return BadRequest(ModelState); //retorna um BadRequest 400 - solicitação invalida
            }
            try
            {
                return Ok(await service.GetAll());
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}