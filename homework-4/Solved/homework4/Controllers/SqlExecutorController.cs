using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Json;
using homework4.Core.DateBase;
using homework4.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper.Json;

namespace homework4.Controllers
{
    [Route("sql")]
    public class SqlExecutorController : ControllerBase
    {
        private readonly SqlExecutor _sqlExecutor;
        
        public SqlExecutorController()
        {
            _sqlExecutor = new SqlExecutor("User ID=****;Password=****;Host=****;Port=***;Database=****;");
        }
        
        [HttpGet("test")]
        public async Task<ActionResult<List<Order>>> GenerateOrderJson()
        {
            var orders = new List<Order>();
            orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                ClientId = 10001,
                CreationDate = DateTime.Now,
                IssueDate = DateTime.Now,
                ItemsData = new Json<Item[]>(new[] {new Item{Id = 5, Count = 2},new Item{Id = 17, Count = 12}}),
                OrderStatusId = 3,
                WarehouseId = 1
            });
            orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                ClientId = 10001,
                CreationDate = DateTime.Now,
                IssueDate = DateTime.Now,
                ItemsData = new Json<Item[]>(new[] {new Item{Id = 5, Count = 2},new Item{Id = 17, Count = 12}}),
                OrderStatusId = 3,
                WarehouseId = 1
            });
            return Ok(orders);
        }
        
        [HttpPost("insertOrders")]
        public async Task<ActionResult<string>> InsertOrders([FromBody] List<Order> orders)
        {
            _sqlExecutor.ExecuteInsertOrders(orders);
            return Ok("insert complete");
        }
        
        [HttpGet("findOrdersByWarehouseId")]
        public IAsyncEnumerable<Order> FindOrdersByWarehouseId([FromQuery] long id)
        {
            return _sqlExecutor.ExecuteFindOrdersByWarehouseId<Order>(id);
        }
        
        [HttpGet("findOrdersByStatusId")]
        public IAsyncEnumerable<Order> FindByOrderStatusId([FromQuery] long id)
        {
           return _sqlExecutor.ExecuteFindOrdersByStatusId<Order>(id);
        }
        
        [HttpGet("findOrdersByPeriod")]
        public IAsyncEnumerable<Order> FindByOrdersByPeriod([FromQuery] DateTime firstDate, DateTime lastDate)
        {
            return _sqlExecutor.ExecuteFindOrdersByPeriod<Order>(firstDate, lastDate);
        }
    }
}