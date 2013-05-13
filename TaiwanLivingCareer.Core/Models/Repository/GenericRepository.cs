using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TaiwanLivingCareer.Core.Models.Interface;
using TaiwanLivingCareer.Core.Models.Entity;

namespace TaiwanLivingCareer.Core.Models.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        private DbContext _context
        {
            get;
            set;
        }
        public GenericRepository()
            : this(new TLCDBEntities())
        {
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = new DbContext(context, true);
        }



        /// <summary>
        /// Creates the specified instance.
        /// bbb
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public void Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add(instance);
                this.SaveChanges();
            }

        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                    new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                    }))
            {
                return this._context.Set<TEntity>().FirstOrDefault(predicate);
            }
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                   new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                   }))
            {
                return this._context.Set<TEntity>().AsQueryable();
            }
        }


        /// <summary>
        /// 查詢資料是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }
                ))
            {
                var s = this._context.Set<TEntity>().FirstOrDefault(predicate);

                return (s == null) ? false : true;
            }
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
    }
}
