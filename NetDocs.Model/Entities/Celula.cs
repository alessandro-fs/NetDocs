using System;
using System.Collections.Generic;

namespace NetDocs.Model.Entities
{
    public class Celula
    {
        public int CelulaId { get; set; }

        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }

        public string UsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string UsuarioAlteracao { get; set; }

        //---
        //Relacionamento com a classe Setor
        public virtual IEnumerable<Setor> Setores { get; set; }
    }
}
