using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data.Utils
{
    public class SqlGenerator<T> : ISqlGenerator<T> where T : class
    {
        public string TableName { get; private set; }

        public SqlGenerator(string tableName)
        {
            TableName = tableName;
        }

        private static IEnumerable<Type> AllowedTypes
        {
            get => new[] { typeof(bool), typeof(DateTime), typeof(int), typeof(string) };
        }

        private IEnumerable<DbColumn> _columns;

        private IEnumerable<DbColumn> Columns
        {
            get
            {
                if (_columns == null)
                {
                    _columns = GetColumns();
                }

                return _columns;
            }
        }

        private IEnumerable<DbColumn> GetColumns()
        {
            var columns = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                .Where(p => AllowedTypes.Any(type => p.PropertyType == type || p.PropertyType == Nullable.GetUnderlyingType(type)))
                .Select(p => new DbColumn(p, p.Name));

            return columns;
        }

        public string SelectAll() => $"SELECT * FROM \"{TableName}\"";

        public string Select() => $"SELECT * FROM \"{TableName}\" WHERE \"Id\" = @Id";

        public string Insert()
        {
            var columnsExceptId = Columns.Where(c => c.Name != "Id");

            var columns = string.Join(", ", columnsExceptId.Select(c => $"\"{c.Name}\""));
            var values = string.Join(", ", columnsExceptId.Select(c => $"@{c.Name}"));
            return $"INSERT INTO \"{TableName}\" ({columns}) VALUES ({values}); SELECT CAST(SCOPE_IDENTITY() AS INT";
        }

        public string Update()
        {
            var columnsExceptId = Columns.Where(c => c.Name != "Id");

            var columns = string.Join(", ", columnsExceptId.Select(c => $"\"{c.Name}\" = @{c.Name}"));
            return $"UPDATE \"{TableName}\" SET {columns} WHERE \"Id\" = @Id";
        }

        public string Delete() => $"DELETE FROM \"{TableName}\" WHERE \"Id\" = @Id";
    }
}
