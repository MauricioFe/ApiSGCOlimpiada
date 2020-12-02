using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class SolicitacaoCompra
    {
        public long Id { get; set; }
        public string ResponsavelEntrega { get; set; }
        public DateTime Data { get; set; }
        public long TipoCompraId { get; set; }
        public long EscolaId { get; set; }
        public TipoCompra TipoCompra { get; set; }
        public Escola Escola { get; set; }
        public string Justificativa { get; set; }
        public string  Anexo { get; set; }
    }
}
