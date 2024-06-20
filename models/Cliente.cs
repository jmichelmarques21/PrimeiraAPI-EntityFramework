using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.models{
    public class Cliente{

        [Key]
        public int Id { get; set; }
        public String? Nome { get; set; }
        public String? Cpf { get; set; }
        public String? Email { get; set; }
    }
}