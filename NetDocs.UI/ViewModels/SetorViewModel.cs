using System;
using System.ComponentModel.DataAnnotations;

namespace NetDocs.UI.ViewModels
{
    public class SetorViewModel
    {
        [Key]
        public int SetorId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        [Required]
        public string UsuarioCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string UsuarioAlteracao { get; set; }

        
        //---
        //Relacionamento com a classe Celula
        public int CelulaId { get; set; }
        public virtual CelulaViewModel Celula { get; set; }
    }
}
