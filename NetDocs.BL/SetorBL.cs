using System.Collections.Generic;
using NetDocs.DAL.Repository;
using NetDocs.Model.Entities;
using System;

namespace NetDocs.BL
{
    public class SetorBL
    {
        private readonly SetorRepository _dal = new SetorRepository();

        public void Add(Setor obj)
        {
            try
            {
                _dal.BeginTransaction();
                _dal.Add(obj);
                _dal.CommitTransaction();
            }
            catch(Exception ex)
            {
                _dal.RollbackTransaction();
                throw ex;
            }
        }

        public Setor GetById(int id)
        {
            return _dal.GetById(id);
        }

        public IEnumerable<Setor> GetAll()
        {
            return _dal.GetAll();
        }

        public void Update(Setor obj)
        {
            _dal.Update(obj);
        }

        public void Remove(Setor obj)
        {
            _dal.Remove(obj);
        }

        public void Dispose()
        {
            if (_dal != null)
                _dal.Dispose();
        }
    }
}
