using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class ProdutoPedidoOrcamento
    {
        public long ProdutoId { get; set; }
        public long SolicitacaoComprasId { get; set; }
        public Produto Produto { get; set; }
        public SolicitacaoCompra SolicitacaoCompra { get; set; }
        public double valor { get; set; }
        public int Quantidade { get; set; }
        public double Ipi { get; set; }
        public double Icms { get; set; }
        public long OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; }
    }
}
