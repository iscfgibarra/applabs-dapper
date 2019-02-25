using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AppLabs.QueryExpression;

namespace AppLabs.Dapper.Interfaces
{
    public interface IRepository<T>
    where T: class 
    {
        
        bool HasError { get; set; }
        string ErrorMessage { get; set; }
        T Find(int id);

        Task<T> FindAsync(int id);

        IEnumerable<T> Find(WrapperExpression predicate);

        Task<IEnumerable<T>> FindAsync(WrapperExpression predicate);

        IEnumerable<T> All();

        IEnumerable<T> GetPage(int inicio, int pageSize);


        Task<IEnumerable<T>> AllAsync();

        Task<IEnumerable<T>> GetPageAsync(int inicio, int pageSize);

        int CreateOrUpdate(T item);

        Task<int> CreateOrUpdateAsync(T item);

        bool Delete(int id);

        Task<bool> DeleteAsync(int id);


        string SelectClause { get;}
        string FromClause { get; }

        string OrderByClause { get; }
        string GetAllQueryName { get; }
        CommandType GetAllQueryType { get; }
        string GetFindQueryName { get; }
        CommandType GetFindQueryType { get; }
        string GetCreateUpdateQueryName { get; }
        CommandType GetCreateUpdateQueryType { get; }
        string GetDeleteQueryName { get; }
        CommandType GetDeleteQueryType { get; }
        string GetPageQueryName { get; }
        CommandType GetPageQueryType { get; }

        object GetCreateUpdateParameters(T item);

        object GetDeleteParameters(int id);

        object GetFindParameters(int id);

        object GetPageParameters(int inicio, int pageSize);

        IDbConnection Connection { get; set; }

        IUnitOfWork Uow { get; set; }
        ExecuteMode ExecuteMode { get; set; }

       

        void InitConnection();
        void FinalizeConnection();
    }
}
