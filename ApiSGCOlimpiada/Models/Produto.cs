﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public int CodigoProtheus { get; set; }
        public string Descricao { get; set; }
        public Grupo Grupo { get; set; }
    }
}
