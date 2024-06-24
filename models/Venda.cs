using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.models
{
    public class Venda
    {   
        [Key]
        public int Id { get; set; }
        public String? NotaFiscal { get; set; }
        public String? DataVenda { get; set; }
        public Cliente Cliente { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public float Preco { get; set; }
    }
}