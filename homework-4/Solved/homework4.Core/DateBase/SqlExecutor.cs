using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace homework4.Core.DateBase
{
    public class SqlExecutor
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public SqlExecutor(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(connectionString);
        }
        
        public List<T> ExecuteFindByWarehouseId<T>(long warehouseId)
        {
            var query = string.Format($"select * from orders where warehouse_id = {warehouseId}");
            using (_connection)
            {
                return _connection.Query<T>(query).ToList();
            }
        }
        
        public List<T> ExecuteFindByOrderStatusId<T>(string)
        {
            var query = string.Format($"select * from orders where warehouse_id = {warehouseId}");
            using (_connection)
            {
                return _connection.Query<T>(query).ToList();
            }
        }
    }
}