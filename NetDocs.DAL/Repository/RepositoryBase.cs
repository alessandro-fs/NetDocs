using NetDocs.DAL.Context;
using NetDocs.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;


namespace NetDocs.DAL.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected NetDocsContextDb Db = new NetDocsContextDb();
        private System.Data.Common.DbTransaction trans;

        public void Add(TEntity obj)
        {
            try
            {
                Db.Set<TEntity>().Add(obj);
                if (trans != null)
                    Db.Database.UseTransaction(trans);

                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public void Update(TEntity obj)
        {
            try
            {
                Db.Entry(obj).State = EntityState.Modified;
                if (trans != null)
                    Db.Database.UseTransaction(trans);
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(TEntity obj)
        {
            
            try
            {
                Db.Set<TEntity>().Remove(obj);
                Db.SaveChanges();
                if (trans != null)
                    Db.Database.UseTransaction(trans);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (Db != null)
                Db.Dispose();
        }

        public void BeginTransaction()
        {
            Db.Database.Connection.Open();
            trans = Db.Database.Connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public void CommitTransaction()
        {
            try
            {
                trans.Commit();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            { 
                CloseConnection(); 
            }            
        }

        public void RollbackTransaction()
        {
            try
            {
                trans.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            } 
        }

        private void CloseConnection()
        {
            if (Convert.ToBoolean(ConnectionState.Open))
                Db.Database.Connection.Close();
        }
    }
}
