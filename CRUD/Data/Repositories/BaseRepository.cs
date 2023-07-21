using Dapper;
using Data.Repositories.Interfaces;
using Data.Utils;
using Domain.Models.Base;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected abstract ISqlGenerator<T> SqlGenerator { get; set; }

        protected string ConnectionString { get; private set; }

        public BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = SqlGenerator.SelectAll();
                var list = await connection.QueryAsync<T>(sql);
                return list;
            }
        }

        public virtual async Task<T> GetById(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = SqlGenerator.Select();
                var entity = await connection.QuerySingleAsync<T>(sql, new { Id = id });
                return entity;
            }
        }

        public virtual async Task<T> Insert(T entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = SqlGenerator.Insert();
                entity.Id = await connection.QuerySingleAsync<int>(sql, entity);
                return entity;
            }
        }

        public virtual async Task<T> Update(T entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = SqlGenerator.Update();
                await connection.ExecuteAsync(sql, entity);
                return entity;
            }
        }

        public virtual async Task Remove(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = SqlGenerator.Delete();
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
