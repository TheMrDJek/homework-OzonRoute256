using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using homework4.Models;
using Npgsql;

namespace homework4.Core.DateBase
{
    public class SqlExecutor
    {
        private readonly string _connectionString;
        private readonly NpgsqlConnection _connection;

        public SqlExecutor(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new NpgsqlConnection(connectionString);
        }

        public void ExecuteInsertOrders<T>(List<T> orders)
        {
            var query = string.Format($"insert into orders values (@Id,@ClientId,@CreationDate,@IssueDate,@OrderStatusId,CAST(@ItemsData as Json),@WarehouseId)");

            using (_connection)
            {
                _connection.Execute(query, orders);
            }
        }

        public async IAsyncEnumerable<Order> ExecuteFindOrdersByWarehouseId<T>(long warehouseId)
        {
            var query = string.Format($"select * from orders where warehouse_id = {warehouseId}");
            var reader = await _connection.ExecuteReaderAsync(query);
            var rowParser = reader.GetRowParser<Order>();

            while (await reader.ReadAsync())
            {
                yield return rowParser(reader);
            }
        }

        public async IAsyncEnumerable<Order> ExecuteFindOrdersByStatusId<T>(long statusId)
        {
            var query = string.Format($"select * from orders where order_status_id = {statusId}");
            var reader = await _connection.ExecuteReaderAsync(query);
            var rowParser = reader.GetRowParser<Order>();

            while (await reader.ReadAsync())
            {
                yield return rowParser(reader);
            }
        }

        public async IAsyncEnumerable<Order> ExecuteFindOrdersByPeriod<T>(DateTime firstDate, DateTime lastDate)
        {
            var firstDateText = firstDate.ToString("yyyy-MM-dd");
            var lastDateText = lastDate.ToString("yyyy-MM-dd");
            var query = string.Format($"select * from orders where creation_date >= '{firstDateText}' and creation_date <= '{lastDateText}'");
            var reader = await _connection.ExecuteReaderAsync(query);
            var rowParser = reader.GetRowParser<Order>();

            while (await reader.ReadAsync())
            {
                yield return rowParser(reader);
            }
        }
        
        
    }
}