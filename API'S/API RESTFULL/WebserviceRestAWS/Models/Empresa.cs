using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Empresa
    {
        public ObjectId Id { get; set; }

        public int codigo { get; set; }

        [Required]
        public string nome { get; set; }

        public string telefone { get; set; }
        [Required]
        public string cnpj { get; set; }

        public DateTime dia { get; set; }
     
    }
}
