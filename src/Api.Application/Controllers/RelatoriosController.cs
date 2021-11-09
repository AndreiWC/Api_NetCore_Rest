using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Reports;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Application.Controllers
{
    //Aqui cria as rotas das api, equivale ao Http:localhost:5000/api/users

    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase //Extende a ControllerBase do pacote Microsoft.AspNetCore.Mvc onde faz o gerenciamento das rotas
    {

        private IReportUserService _service;
        // contrutor criado para passar o  IUserService para dentro da classe
        public RelatoriosController(IReportUserService service)
        {
            _service = service;
        }
        //AQUI INICIA OS METODOS DA API RESTFULL
        //METODO DO TIPO GETALL, RETORNA TODOS OS DADOS 
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetListagemSimples()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var webRepor = new WebReport();
            string filePath = Path.Combine("src\\Reports\\User.frx");
            webRepor.Report.Load(filePath);

            var users =new DataTable();
            var usersList = await _service.GetListagemSimples();

            users.Columns.Add("Id", typeof(string));
            users.Columns.Add("Nome", typeof(string));
            users.Columns.Add("Email", typeof(string));


            foreach (var item in usersList)
            {
                users.Rows.Add(item.Id, item.Name, item.Email);
            }

            webRepor.Report.RegisterData(users, "Users");
            webRepor.Report.Prepare();

            byte[] repostArray = null;

            using(MemoryStream ms = new MemoryStream()){
                var pdfExport = new PDFSimpleExport();

                pdfExport.Export(webRepor.Report,ms);
                ms.Flush();
                repostArray = ms.ToArray();

            }

            return File(repostArray, "application/pdf", "User.pdf");

        }
    }
}