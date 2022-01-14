using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using WebApplication1.Models;

//##CONTROLER DE EMPRESAS##

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmpresaController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // Faz a parte de consulta de todos os registros no banco

        [HttpGet]
        public JsonResult Get(int codigo)
        {

            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var dbList = dbClient.GetDatabase("Webservicedatabase").GetCollection<Empresa>("Empresa").AsQueryable();

            var filter = Builders<Pessoa>.Filter.Eq("codigo", codigo);

            return new JsonResult(dbList);
        }

        // Faz a parte de inserção no banco
            //*!Fazer verificação da query!*

        [HttpPost]
        public JsonResult Post(Empresa emp)
        {


            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            int LastEmpresaId = dbClient.GetDatabase("Webservicedatabase").GetCollection<Pessoa>("Empresa").AsQueryable().Count();
            emp.codigo = LastEmpresaId + 1;

            dbClient.GetDatabase("Webservicedatabase").GetCollection<Empresa>("Empresa").InsertOne(emp);

            return new JsonResult("Documento adicionado com sucesso");
        }

        // Faz a parte de update da database
        [HttpPut]
        public JsonResult Put(Empresa emp)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var filter = Builders<Empresa>.Filter.Eq("codigo", emp.codigo);

            var update = Builders<Empresa>.Update.Set("nome", emp.nome)
                                                    .Set("telefone", emp.telefone)
                                                    .Set("cnpj", emp.cnpj);

            dbClient.GetDatabase("Webservicedatabase").GetCollection<Empresa>("Empresa").UpdateOne(filter, update);

            return new JsonResult("Documento atulizado com sucesso");
        }

        // Faz a parte do delete de resgitros
        [HttpDelete("{codigo}")]
        public JsonResult Delete(int codigo)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var filter = Builders<Empresa>.Filter.Eq("codigo", codigo);

            dbClient.GetDatabase("Webservicedatabase").GetCollection<Empresa>("Empresa").DeleteOne(filter);


            return new JsonResult("Documento deletado com sucesso");


        }
    }
}
