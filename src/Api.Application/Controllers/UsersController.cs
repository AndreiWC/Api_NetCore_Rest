using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    //Aqui cria as rotas das api, equivale ao Http:localhost:5000/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase //Extende a ControllerBase do pacote Microsoft.AspNetCore.Mvc onde faz o gerenciamento das rotas
    {

        private IUserService _service;
        // contrutor criado para passar o  IUserService para dentro da classe
        public UsersController(IUserService service)
        {
            _service = service;
        }
        //AQUI INICIA OS METODOS DA API RESTFULL
        //METODO DO TIPO GETALL, RETORNA TODOS OS DADOS 
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid) // QUANDO MANDA UMA INFORMAÇÃO PARA ROTA ELA PREENCHE  O ModelState, AQUI É VERIFICADO SE A INFORMAÇÃO É VALIDA
            {
                return BadRequest(ModelState); //retorna um BadRequest 400 - solicitação invalida
            }
            try
            {
                return Ok(await _service.GetAll()); // RETORNA UMA LISTA COM TODOS OS REGISTROS DA TABELA
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); // CASO CAIA NO TRY RETORNA 500
            }
        }
        // 
        //METÓDO DO TIPO GET, RETORNA UM REGISTRO ESPECIFICO, ONDE É RECEBIDO UMA PARAMETRO ID
        [HttpGet]
        [Route("{id}", Name = "GetWithId")] //CONFIGURA A ROTA COM UM PARÂMETRO ID E COM O NOME=GetWithId
        public async Task<ActionResult> Get(Guid id)
        {

            if (!ModelState.IsValid) // QUANDO MANDA UMA INFORMAÇÃO PARA ROTA ELA PREENCHE  O ModelState, AQUI É VERIFICADO SE A INFORMAÇÃO É VALIDA
            {
                return BadRequest(ModelState);//retorna um BadRequest 400 - solicitação invalida
            }

            try
            {
                return Ok(await _service.Get(id)); // RETORNA O REGISTRO ESPECIFICO CORRESPONDENTE AO ID
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); // CASO CAIA NO TRY RETORNA 500
            }

        }
        // CRIA O METÓDO POST, ONDE É RECEBIDO UMA ENTIDADE FORMATO JASON PARA A INSERÇÃO NO BANOC
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid) // QUANDO MANDA UMA INFORMAÇÃO PARA ROTA ELA PREENCHE  O ModelState, AQUI É VERIFICADO SE A INFORMAÇÃO É VALIDA
            {
                return BadRequest(ModelState);//retorna um BadRequest 400 - solicitação invalida
            }

            try
            {
                var result = await _service.Post(user); // ALIMENTA A VARIAVEL COM O RESULTADO DO POST QUE É UMA  USERENTITY

                if (result != null) // VALIDA SE É DIFERENTE DE NULO, CASO SEJA SIGNIFICA QUE O INSERT FOI BEM SUCEDIDA
                {
                    return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result); //RETORNA UMA URL CONTENDO O LINK DE ACESSO DO REGISTRO CRIADO DENTRO DA TABELA USER
                }
                else
                {
                    return BadRequest(); // CASO O RESULT SEJA NULO RETORNA UM BADREQUEST
                }
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); // CASO CAIA NO TRY RETORNA 500
            }

        }



    }
}