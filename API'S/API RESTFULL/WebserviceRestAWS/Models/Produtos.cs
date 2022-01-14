using MongoDB.Bson;
using System;

namespace WebApplication1.Models
{
    public class Produtos
    {



        //public Guid ID { get; set; }
        public ObjectId Id { get; set; }
        public int codigo { get; set; }
        public string nome { get; set; }
        public float quanto { get; set; }
        public string ncm { get; set; }
        public DateTime dia { get; set; }
        public string cpf_cadastro { get; set; }

    }
}
