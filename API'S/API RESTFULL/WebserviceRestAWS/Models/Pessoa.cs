using MongoDB.Bson;
using System;

namespace WebApplication1.Models
{
    public class Pessoa
    {
        public ObjectId Id { get; set; }
        public int codigo { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string cpf { get; set; }
        public DateTime dia { get; set; }
       public string cnpj_empresa {get; set;} 
    }

}
