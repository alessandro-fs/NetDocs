using NetDocs.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace NetDocs.DAL.EntityConfig
{
    public class SetorConfig : EntityTypeConfiguration<Setor>
    {
        public SetorConfig()
        {
            HasKey(c => c.SetorId);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.UsuarioCadastro)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.DataCadastro)
                .IsRequired();

            Property(c => c.DataAlteracao)
                .IsOptional();

            Property(c => c.UsuarioAlteracao)
                .IsOptional()
                .HasMaxLength(50);

            //Tratando o relacionamento com Celula (1_Celula X N_Setor)
            HasRequired(c => c.Celula)
                .WithMany()
                .HasForeignKey(c => c.CelulaId);

        }
    }
}
