using System;

namespace NetDocs.Model.Entities
{
    public class Setor
    {
        public int SetorId { get; set; }

        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }

        public string UsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string UsuarioAlteracao { get; set; }

        //---
        //Relacionamento com a classe Celula
        public int CelulaId { get; set; }
        public virtual Celula Celula { get; set; }
    }
}
