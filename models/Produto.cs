using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.models
{       
        public class Produto{
            [Key]
        public int Id { get; set; }
        public String? Nome { get; set; }
        public Double Preco { get; set; }
        public String? Fornecedor { get; set; }
        
    }
}