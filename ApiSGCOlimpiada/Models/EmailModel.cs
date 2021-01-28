using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class EmailModel
    {
        public string CodUnidadeOrganizacional { get; set; }
        public string CentroResponsabilidade { get; set; }
        public string ClasseValor { get; set; }
        public string UnidadeOrganizacional { get; set; }
        public Responsavel Responsavel { get; set; }
        public Orcamento Orcamento { get; set; }
        public Acompanhamento Acompanhamento { get; set; }
    }
}
