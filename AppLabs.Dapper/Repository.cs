using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppLabs.Dapper.Interfaces;
using AppLabs.QueryExpression;
using Dapper;

namespace AppLabs.Dapper
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IProviderFactory ProviderFactory { get; set; }

        public Repository(IProviderFactory providerFactory)
        {
            ProviderFactory = providerFactory;            
            ExecuteMode = ExecuteMode.Single;
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            Uow = unitOfWork;
            Connection = Uow.Connection;
            ExecuteMode = ExecuteMode.UnitOfWork;
        }
        
        
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

        public virtual string SelectClause => "*";

        public virtual string FromClause => "";

        public virtual string OrderByClause => "";
       
        public virtual string GetAllQueryName => $"SELECT {SelectClause} FROM {FromClause}" + (!string.IsNullOrEmpty(OrderByClause) ? " ORDER BY " + OrderByClause : String.Empty) ;

        public virtual CommandType GetAllQueryType => CommandType.Text;

        public virtual string GetFindQueryName => $"SELECT {SelectClause} FROM {FromClause}" + (!string.IsNullOrEmpty(OrderByClause) ? " ORDER BY " + OrderByClause : String.Empty);
        public virtual  CommandType GetFindQueryType => CommandType.Text;
        public virtual string GetCreateUpdateQueryName => "";
        public virtual CommandType GetCreateUpdateQueryType => CommandType.Text;

        public virtual string GetDeleteQueryName => "";
        public virtual CommandType GetDeleteQueryType => CommandType.Text;

        public virtual string GetPageQueryName => "";
        public virtual CommandType GetPageQueryType => CommandType.Text;


        public int CreateOrUpdate(T item)
        {
            if (string.IsNullOrEmpty(GetCreateUpdateQueryName))
                throw new ArgumentException(
                    "La propiedad GetCreateUpdateQueryName, no se ha establecido." +
                           "\n Ejem. GetCreateUpdateQueryName => \"UPDATE TABLENAME SET CAMPO1 = '1' WHERE CAMPO2 = 'ALGO' ");
            int idSig = 0;

            try
            {
                InitConnection();

                idSig =  Connection.Query<int>(GetCreateUpdateQueryName, 
                    GetCreateUpdateParameters(item),
                    commandType: GetCreateUpdateQueryType, 
                    transaction: Uow?.Transaction).First();

            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return idSig;
        }


        public async Task<int> CreateOrUpdateAsync(T item)
        {
            int idSig = 0;

            if (string.IsNullOrEmpty(GetCreateUpdateQueryName))
                throw new ArgumentException(
                    "La propiedad GetCreateUpdateQueryName, no se ha establecido." +
                    "\n Ejem. GetCreateUpdateQueryName => \"UPDATE TABLENAME SET CAMPO1 = '1' WHERE CAMPO2 = 'ALGO' ");



            try
            {
                InitConnection();

                var result = await Connection.QueryAsync<int>(GetCreateUpdateQueryName,
                    GetCreateUpdateParameters(item),
                    commandType: GetCreateUpdateQueryType,
                    transaction: Uow?.Transaction);


                idSig = result.First();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return idSig;
        }

        public bool Delete(int id)
        {
            bool retval = false;


            if (string.IsNullOrEmpty(GetDeleteQueryName))
                throw new ArgumentException(
                    "La propiedad GetDeleteQueryName, no se ha establecido." +
                    "\n Ejem. GetDeleteQueryName => \"DELETE FROM TABLENAME WHERE id = @id \"");


            try
            {                
                InitConnection();
                Connection.Query(GetDeleteQueryName, 
                    GetDeleteParameters(id), 
                    commandType: GetDeleteQueryType,
                    transaction: Uow?.Transaction);
                retval = true;
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return retval;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool retval = false;


            if (string.IsNullOrEmpty(GetDeleteQueryName))
                throw new ArgumentException(
                    "La propiedad GetDeleteQueryName, no se ha establecido." +
                    "\n Ejem. GetDeleteQueryName => \"DELETE FROM TABLENAME WHERE id = @id \"");

            try
            {
                InitConnection();
                
                await Connection.QueryAsync(GetDeleteQueryName,
                    GetDeleteParameters(id),
                    commandType: GetDeleteQueryType,
                    transaction: Uow?.Transaction);
                retval = true;
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return retval;
        }

       

        public IEnumerable<T> All()
        {
            List<T> list = new List<T>();

            if (string.IsNullOrEmpty(GetAllQueryName))
                throw new ArgumentException(
                    "La propiedad GetAllQueryName, no se ha establecido." +
                    "\n Ejem. GetAllQueryName => \"SELECT * FROM TABLENAME\"");

            try
            {
                InitConnection();

                list = Connection.Query<T>(GetAllQueryName, GetFindParameters(0), 
                    commandType: GetAllQueryType,
                    transaction: Uow?.Transaction).ToList();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return list;
        }


        public IEnumerable<T> GetPage(int inicio, int pageSize)
        {
            List<T> list = new List<T>();


            if (string.IsNullOrEmpty(GetPageQueryName))
                throw new ArgumentException(
                    "La propiedad GetPageQueryName, no se ha establecido." +
                    "\n Ejem. GetPageQueryName => \"SELECT LIMIT 10 * FROM TABLENAME \"");


            try
            {
                InitConnection();

                list = Connection.Query<T>(GetPageQueryName, GetPageParameters(inicio, pageSize),
                    commandType: GetPageQueryType,
                    transaction: Uow?.Transaction).ToList();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return list;
        }



        public async Task<IEnumerable<T>> AllAsync()
        {
            List<T> list = new List<T>();

            if (string.IsNullOrEmpty(GetAllQueryName))
                throw new ArgumentException(
                    "La propiedad GetAllQueryName, no se ha establecido." +
                    "\n Ejem. GetAllQueryName => \"SELECT * FROM TABLENAME\"");

            try
            {
                InitConnection();

                var result = await Connection.QueryAsync<T>(GetAllQueryName, GetFindParameters(0),
                    commandType: GetAllQueryType , transaction: Uow?.Transaction);

                list = result.ToList();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return list;
        }

        public async Task<IEnumerable<T>> GetPageAsync(int inicio, int pageSize)
        {
            List<T> list = new List<T>();


            if (string.IsNullOrEmpty(GetPageQueryName))
                throw new ArgumentException(
                    "La propiedad GetPageQueryName, no se ha establecido." +
                    "\n Ejem. GetPageQueryName => \"SELECT LIMIT 10 * FROM TABLENAME \"");

            try
            {
                InitConnection();

                var result = await Connection.QueryAsync<T>(GetPageQueryName, GetPageParameters(inicio, pageSize),
                    commandType: GetPageQueryType, transaction: Uow?.Transaction);

                list = result.ToList();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return list;
        }

        public virtual IEnumerable<T> Find(WrapperExpression predicate)
        {
            List<T> items = new List<T>();

            try
            {
                InitConnection();

                string selectQuery = $"SELECT {SelectClause} FROM {FromClause} WHERE {predicate.ToWhereClause()}";

                selectQuery += !string.IsNullOrEmpty(OrderByClause) ? $" ORDER BY {OrderByClause}" : String.Empty;

                var result = Connection.Query<T>(selectQuery, commandType: CommandType.Text);

                return result.ToList();

            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return items;
        }

        public virtual async Task<IEnumerable<T>> FindAsync(WrapperExpression predicate)
        {           
            List<T> items = new List<T>();

            try
            {
                InitConnection();
                
                string selectQuery = $"SELECT {SelectClause} FROM {FromClause} WHERE {predicate.ToWhereClause()}";

                selectQuery += !string.IsNullOrEmpty(OrderByClause) ? $" ORDER BY {OrderByClause}" : String.Empty;

                var result = await Connection.QueryAsync<T>(selectQuery, commandType: CommandType.Text);

                return result.ToList();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return items;
        }

        public T Find(int id)
        {
            T itemFounded = null;

            try
            {
                InitConnection();

                itemFounded = Connection.Query<T>(GetFindQueryName, GetFindParameters(id),
                    commandType: CommandType.StoredProcedure, transaction: Uow?.Transaction).FirstOrDefault();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return itemFounded;
        }

        public async Task<T> FindAsync(int id)
        {
            T itemFounded = null;

            try
            {
                InitConnection();

                var result = await Connection.QueryAsync<T>(GetFindQueryName, GetFindParameters(id),
                    commandType: CommandType.StoredProcedure, transaction: Uow?.Transaction);

                itemFounded = result.FirstOrDefault();
            }
            catch (Exception e)
            {
                HasError = true;
                ErrorMessage = e.Message;
            }
            finally
            {
                FinalizeConnection();
            }

            return itemFounded;
        }

        public virtual object GetCreateUpdateParameters(T item)
        {
            return null;
        }

        public virtual object GetDeleteParameters(int id)
        {
            return null;
        }

        public virtual object GetFindParameters(int id)
        {
            return null;
        }

        public virtual object GetPageParameters(int inicio, int pageSize) => new
        {
            Inicio = inicio,
            PageSize = pageSize
        };

        public IDbConnection Connection { get; set; }

        public IUnitOfWork Uow { get; set; }

        public ExecuteMode ExecuteMode { get; set; }
        public void InitConnection()
        {
            if (ExecuteMode == ExecuteMode.Single)
            {
                if (Connection == null)
                    Connection = ProviderFactory.GetDefaultProvider().GetConnection();

                Connection.Open();
            }
        }

        public void FinalizeConnection()
        {
            if (ExecuteMode == ExecuteMode.Single)
            {
                Connection?.Close();
            }
        }
    }
}
