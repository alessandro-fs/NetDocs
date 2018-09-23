using NetDocs.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NetDocs.DAL.Repository
{
    public class SetorRepository : RepositoryBase<Setor>
    {
        public IEnumerable<Setor> BuscarSetorPorNome(string nome)
        {
            //POSSO USAR O Db PORQUE FOI CRIADO COMO PROTECTED
            return Db.Setor.Where(p => p.Nome == nome);
        }
    }
}
