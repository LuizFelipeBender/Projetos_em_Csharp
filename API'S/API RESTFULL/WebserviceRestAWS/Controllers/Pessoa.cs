using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        string cpf;

        private readonly IConfiguration _configuration;
        public PessoaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("codigo")]
        
        public JsonResult Get(int codigo)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var dbList = dbClient.GetDatabase("Webservicedatabase").GetCollection<Pessoa>("Pessoa").AsQueryable();
            
            var filter = Builders<Pessoa>.Filter.Eq("codigo", codigo);
            
            return new JsonResult(dbList);

                }

        [HttpPost]
              
        public JsonResult Post(Pessoa pes)
        {
            
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            int LastPessoaId = dbClient.GetDatabase("Webservicedatabase").GetCollection<Pessoa>("Pessoa").AsQueryable().Count();
            pes.codigo = LastPessoaId + 1;
           
            



            dbClient.GetDatabase("Webservicedatabase").GetCollection<Pessoa>("Pessoa").InsertOne(pes);

            return new JsonResult("Documento adicionado com sucesso");
        }

        [HttpPut]
        public JsonResult Put(Pessoa pes)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var filter = Builders<Pessoa>.Filter.Eq("codigo", pes.codigo);

            var update = Builders<Pessoa>.Update.Set("nome", pes.nome)
                                                    .Set("cpf",pes.cpf)
                                                    .Set("telefone", pes.telefone)
                                                    .Set("cnpj", pes.cnpj_empresa);



            dbClient.GetDatabase("Webservicedatabase").GetCollection<Pessoa>("Pessoa").UpdateOne(filter, update);

            return new JsonResult("Documento atualizado com sucesso");
        }


        [HttpDelete("{codigo}")]
        public JsonResult Delete(int codigo)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("Connection"));

            var filter = Builders<Pessoa>.Filter.Eq("codigo", codigo);


            dbClient.GetDatabase("Webservicedatabase").GetCollection<Pessoa>("Pessoa").DeleteOne(filter);

            return new JsonResult("Documento deletado com sucesso");
        }



    }
}
