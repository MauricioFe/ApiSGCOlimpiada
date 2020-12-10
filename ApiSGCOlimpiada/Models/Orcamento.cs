using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class Orcamento
    {
        public long Id { get; set; }
        public string Fornecedor { get; set; }
        public string Cnpj { get; set; }
        public double ValorTotal { get; set; }
        public double TotalIpi { get; set; }
        public double TotalProdutos { get; set; }
        public string Anexo { get; set; }
        public DateTime Data { get; set; }
        public string FormaPagamento { get; set; }
        public double ValorFrete { get; set; }
    }
}
