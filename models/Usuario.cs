using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.models
{
    public class Usuario
    {   
        [Key]
        public int Id { get; set; }
        public String? Nome { get; set; }
        public String? Email { get; set; }
        public String? Senha { get; set; }
    }
}