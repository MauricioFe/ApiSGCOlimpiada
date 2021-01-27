using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class EmailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Ocupacao { get; set; }
        public string Competidor { get; set; }
        public string avaliador { get; set; }
        public string CodUnidadeOrganizacional { get; set; }
        public string CentroResponsabilidade { get; set; }
        public string ClasseValor { get; set; }
        public string Cnpj { get; set; }
        public string UnidadeOrganizacional { get; set; }
        public string Orcamento { get; set; }
        public string Valor { get; set; }
    }
}
