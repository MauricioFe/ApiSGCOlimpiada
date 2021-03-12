using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class ProdutoPedidoOrcamento
    {
        public long Id { get; set; }
        public decimal valor { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Ipi { get; set; }
        public decimal Icms { get; set; }
        public decimal Desconto { get; set; }
        public decimal TotalItem { get; set; }
        public Orcamento Orcamento { get; set; }
        public ProdutoSolicitacao ProdutoSolicitacao { get; set; }
        public long ProdutoSolicitacoesId { get; set; }
        public long OrcamentoId { get; set; }
    }
}
