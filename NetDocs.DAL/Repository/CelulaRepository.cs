using NetDocs.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NetDocs.DAL.Repository
{
    public class CelulaRepository : RepositoryBase<Celula>
    {
        public IEnumerable<Celula> BuscarCelulaPorNome(string nome)
        {
            //POSSO USAR O Db PORQUE FOI CRIADO COMO PROTECTED
            return Db.Celula.Where(p => p.Nome == nome);
        }
    }
}
