using NetDocs.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace NetDocs.DAL.EntityConfig
{
    public class CelulaConfig : EntityTypeConfiguration<Celula>
    {
        public CelulaConfig()
        {
            HasKey(c => c.CelulaId);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.UsuarioCadastro)
                .IsOptional()
                .HasMaxLength(50);

            Property(c => c.DataCadastro)
                .IsOptional();            

            Property(c => c.UsuarioAlteracao)
                .IsOptional()
                .HasMaxLength(50);

            Property(c => c.DataAlteracao)
                .IsOptional();
        }
    }
}
