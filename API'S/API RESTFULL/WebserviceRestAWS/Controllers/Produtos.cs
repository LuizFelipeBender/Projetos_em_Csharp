using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProdutosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpPost]
        public JsonResult Post(Produtos pes)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            int LastProdutosId = dbClient.GetDatabase("Webservicedatabase").GetCollection<Produtos>("Produtos").AsQueryable().Count();
            pes.codigo = LastProdutosId + 1;
            dbClient.GetDatabase("Webservicedatabase").GetCollection<Produtos>("Produtos").InsertOne(pes);

            return new JsonResult("Documento adicionado com sucesso");
        }

        [HttpGet]
        public JsonResult Get(int codigo)
        {

            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var dbList = dbClient.GetDatabase("Webservicedatabase").GetCollection<Produtos>("Produtos").AsQueryable();

            var filter = Builders<Produtos>.Filter.Eq("codigo", codigo);

            return new JsonResult(dbList);
        }
        

        [HttpPut]
        public JsonResult Put(Produtos pes)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var filter = Builders<Produtos>.Filter.Eq("codigo", pes.codigo);

            var update = Builders<Produtos>.Update.Set("nome", pes.nome);



            dbClient.GetDatabase("Webservicedatabase").GetCollection<Produtos>("Produtos").UpdateOne(filter, update);

            return new JsonResult("Documento atualizado com sucesso");
        }


        [HttpDelete("{codigo}")]
        public JsonResult Delete( int codigo)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var filter = Builders<Produtos>.Filter.Eq("codigo", codigo);

            dbClient.GetDatabase("Webservicedatabase").GetCollection<Produtos>("Produtos").DeleteOne(filter);


            return new JsonResult("Documento deletado com sucesso");


        }





    }
}


