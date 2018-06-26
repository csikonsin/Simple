using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simple.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();

        IArticleRepository ArticleRepository { get; }
        //IModuleRepository ModuleRepository { get; }
    }

    public sealed class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            Id = Guid.NewGuid();
            Connection = Data.Connection.GetSql();
        }

        internal UnitOfWork(IDbConnection connection)
        {
            Id = Guid.NewGuid();
            Connection = connection;
        }

        internal UnitOfWork(string connectionString)
        {
            Id = Guid.NewGuid();
            Connection = new System.Data.SqlClient.SqlConnection(connectionString);
        }

        public Guid Id { get; } = Guid.Empty;
        public IDbConnection Connection { get; private set; } = null;
        public IDbTransaction Transaction { get; private set; } = null;
        private bool _disposed;

        private IArticleRepository _articleRepository;
        public IArticleRepository ArticleRepository { get { return _articleRepository ?? (_articleRepository = new ArticleRepository(this)); } }

        //private IModuleRepository _moduleRepository;
        //public IModuleRepository ModuleRepository { get { return _moduleRepository ?? (_moduleRepository = new ModuleRepository(this)); } }






        private void ResetRepositories()
        {
            _articleRepository = null;
            //_moduleRepository = null;
        }

        public void Begin()
        {
            Transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            catch (Exception)
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                Transaction.Dispose();
                Transaction = Connection.BeginTransaction();
                ResetRepositories();
            }

        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Transaction != null)
                    {
                        Transaction.Dispose();
                        Transaction = null;
                    }
                    if (Connection != null)
                    {
                        Connection.Dispose();
                        Connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}