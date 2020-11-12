using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class ProdutoPedidoOrcamento
    {
        public int ProdutoId { get; set; }
        public int SolicitacaoComprasId { get; set; }
        public Produto Produto { get; set; }
        public SolicitacaoCompra SolicitacaoCompra { get; set; }
        public double valor { get; set; }
        public int Quantidade { get; set; }
        public double Ipi { get; set; }
        public double Icms { get; set; }
        public int OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; }
    }
}
