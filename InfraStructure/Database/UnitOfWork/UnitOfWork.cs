using Helpers;
using InfraStructure.Database.Repository;
using InfraStructure.Database.RepositoryFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace InfraStructure.Database.UnitOfWork
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>
     where TContext : DbContext, IDisposable
    {
        private Dictionary<(Type type, string name), object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return (IRepository<TEntity>)GetOrAddRepository(typeof(TEntity), new Repository<TEntity>(Context));
        }


        public TContext Context { get; }
        //public ProjectManagementToolContext dbContextTransaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbContextTransaction dbContextTransaction { get; set; }
        public int Commit(bool autoHistory = false)
        {
            if (autoHistory) Context.EnsureAutoHistory();

            var status = Context.SaveChanges();
            Context.ChangeTracker.Clear();
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync(bool autoHistory = false)
        {
            if (autoHistory) Context.EnsureAutoHistory();
            try
            {
                var status = await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
            Context.ChangeTracker.Clear();
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        internal object GetOrAddRepository(Type type, object repo)
        {
            _repositories ??= new Dictionary<(Type type, string Name), object>();

            if (_repositories.TryGetValue((type, repo.GetType().FullName), out var repository)) return repository;
            _repositories.Add((type, repo.GetType().FullName), repo);
            return repo;
        }

        public async Task<int> CommitAsyncWithTransaction()
        {
            try
            {
                // var transaction = Context.Database.BeginTransaction();
                //using (var transaction = Context.Database.BeginTransaction())
                // {
                try
                {

                    if (dbContextTransaction == null)
                        dbContextTransaction = Context.Database.BeginTransaction();
                    // else
                    //  transaction = dbContextTransaction;

                    int result = await Context.SaveChangesAsync();
                    //  transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    // transaction.Rollback();
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException != null ? ex.InnerException.ToString() : "");
                }
               

            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException != null ? ex.InnerException.ToString() : "");               
            }
        }
    }
}
