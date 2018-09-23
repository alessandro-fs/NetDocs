using NetDocs.DAL.EntityConfig;
using NetDocs.Model.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
namespace NetDocs.DAL.Context
{
    public class NetDocsContextDb: DbContext
    {
        public NetDocsContextDb()
            : base("NetDocsConn")
        {
            // This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<Celula> Celula { get; set; }
        public DbSet<Setor> Setor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //--
            //CONFIGURAR POR DEFAULT PK NA TABELA QUE TERMINE COM ID
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            //--
            //CONFIGURAR PARA CAMPO STRING VIR COMO VARCHAR E NÃO CHAR
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            //--
            //QUANDO NÃO DISSER O VALOR DA STRING, POR PADRÃO SERÁ 100
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new CelulaConfig());
            modelBuilder.Configurations.Add(new SetorConfig());

        }

        //--
        //SOBRESCREVER MÉTODO SAVECHANGES PARA CAMPO DATA CADASTRO
        public override int SaveChanges()
        {
            try
            {
                #region DataCadastro, DataAlteracao
                foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataCadastro").IsModified = false;
                    }
                }

                foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataAlteracao").IsModified = false;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    }
                }
                #endregion

                #region UsuarioCadastro, UsuarioAlteracao
                foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UsuarioCadastro") != null))
                {                    
                    if (entry.State == EntityState.Modified)
                        entry.Property("UsuarioCadastro").IsModified = false;
                }

                foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UsuarioAlteracao") != null))
                {
                    if (entry.State == EntityState.Added)
                        entry.Property("UsuarioAlteracao").IsModified = false;
                }
                #endregion

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Exception _exception = ex;
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        _exception = new InvalidOperationException(message, _exception);
                    }
                }
                throw _exception;
            }
            
        }
    }
}