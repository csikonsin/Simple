using System;
using System.Data;

namespace Simple.Data
{
    public interface IUnitOfWork
    {
        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();

        IArticleRepository ArticleRepository { get; }
        IModuleRepository ModuleRepository { get; }
        IMenuRepository MenuRepository { get; }
        IWebsiteRepository WebsiteRepository { get; }
    }

    public sealed class UnitOfWork : IUnitOfWork, IDisposable
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

        private IModuleRepository _moduleRepository;
        public IModuleRepository ModuleRepository { get { return _moduleRepository ?? (_moduleRepository = new ModuleRepository(this)); } }

        private IMenuRepository _menuRepository;
        public IMenuRepository MenuRepository { get { return _menuRepository ?? (_menuRepository = new MenuRepository(this)); } }

        private IWebsiteRepository _websiteRepository;
        public IWebsiteRepository WebsiteRepository { get { return _websiteRepository ?? (_websiteRepository = new WebsiteRepository(this)); } }

        private void ResetRepositories()
        {
            _articleRepository = null;
            _moduleRepository = null;
            _menuRepository = null;
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