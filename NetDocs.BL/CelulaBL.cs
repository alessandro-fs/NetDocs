using System.Collections.Generic;
using NetDocs.DAL.Repository;
using NetDocs.Model.Entities;
using System;
namespace NetDocs.BL
{
    public class CelulaBL 
    {
        private readonly CelulaRepository _dal = new CelulaRepository();

        public void Add(Celula obj)
        {
            try
            {
                _dal.BeginTransaction();
                _dal.Add(obj);
                _dal.CommitTransaction();
            }
            catch (Exception ex)
            {
                _dal.RollbackTransaction();
                throw ex;
            }
        }

        public Celula GetById(int id)
        {
            return _dal.GetById(id);
        }

        public IEnumerable<Celula> GetAll()
        {
            return _dal.GetAll();
        }

        public void Update(Celula obj)
        {
            _dal.Update(obj);
        }

        public void Remove(Celula obj)
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
