using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace NetDocs.UI.ViewModels
{
    public class CelulaViewModel
    {
        [Key]
        public int CelulaId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource1), ErrorMessageResourceName = "VSNome")]
        [Display(ResourceType = typeof(Resources.Resource1), Name = "Nome")]        
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource1), ErrorMessageResourceName = "VSDataCadastro")]
        [Display(ResourceType = typeof(Resources.Resource1), Name = "DataCadastro")]        
        public DateTime DataCadastro { get; set; }

        [Display(ResourceType = typeof(Resources.Resource1), Name = "UsuarioCadastro")]
        public string UsuarioCadastro { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(Resources.Resource1), Name = "DataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [Display(ResourceType = typeof(Resources.Resource1), Name = "UsuarioAlteracao")]
        public string UsuarioAlteracao { get; set; }

        //---
        //Relacionamento com a classe Setor
        public virtual IEnumerable<SetorViewModel> Setores { get; set; }
    
    }
}
