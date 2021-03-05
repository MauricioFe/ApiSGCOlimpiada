using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class Planilha
    {
        public Produto Produto { get; set; }
        public List<ProdutoPedidoOrcamento> ProdutoPedidoOrcamentosList { get; set; }
    }
}
