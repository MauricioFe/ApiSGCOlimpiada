using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class ProdutoPedidoOrcamento
    {
        public long Id { get; set; }
        public double valor { get; set; }
        public int Quantidade { get; set; }
        public double Ipi { get; set; }
        public double Icms { get; set; }
        public double Desconto { get; set; }
        public Orcamento Orcamento { get; set; }
        public ProdutoSolicitacao ProdutoSolicitacao { get; set; }
        public long ProdutoSolicitacoesId { get; set; }
        public long OrcamentoId { get; set; }
    }
}
