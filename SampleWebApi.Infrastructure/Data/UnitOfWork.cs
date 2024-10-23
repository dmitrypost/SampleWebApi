namespace SampleWebApi.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        GenericRepository<User> UserRepository { get; }
        void Save();
        void Dispose();
    }

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private GenericRepository<User> _userRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
