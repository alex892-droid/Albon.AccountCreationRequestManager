namespace Albon.AccountCreationRequestManager
{
    public interface IDatabaseService
    {
        public void Add<T>(T entity);

        public IQueryable<T> Query<T>();

        public void Delete<T>(T entity);
    }
}
