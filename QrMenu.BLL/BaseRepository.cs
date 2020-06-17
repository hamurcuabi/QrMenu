using QrMenu.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.BLL
{
    public class BaseRepository<T> : IDisposable where T : class, new()
    {

        protected readonly QrMenuContext context = new QrMenuContext();


        public ICollection<T> GetList(Expression<Func<T, bool>> filtre = null)
        {
            if (filtre == null)
            {
                return context.Set<T>().ToList();
            }
            else
            {
                return context.Set<T>().Where(filtre).ToList();
            }
        }


        public T GetOne(Expression<Func<T, bool>> filtre)
        {
            return context.Set<T>().FirstOrDefault(filtre);
        }

        public bool Create(T model)
        {
            var created = context.Entry<T>(model);
            created.State = EntityState.Added;
            return context.SaveChanges() > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            var found = context.Set<T>().Find(id);
            var deleted = context.Entry<T>(found);
            deleted.State = EntityState.Deleted;
            return context.SaveChanges() > 0 ? true : false;
        }

        public bool Update(T model)
        {
            var updated = context.Entry<T>(model);
            updated.State = EntityState.Modified;
            return context.SaveChanges() > 0 ? true : false;

        }





























        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BaseRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}