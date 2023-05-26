using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Dapper.Json;
using Npgsql.Internal.TypeHandlers;

namespace homework4.Models
{
    public class Order
    {
        [Column(name:"id")]
        public Guid Id {get;set;}

        [Column(name:"client_id")]
        public long ClientId { get; set; }
        
        [Column(name:"creation_date")]
        public DateTime CreationDate { get; set; }

        [Column(name:"issue_date")]
        public DateTime IssueDate { get; set; }

        [Column(name:"order_status_id")]
        public long OrderStatusId { get; set; }

        [Column(name:"items_data")]
        public Json<Item[]> ItemsData { get; set; }

        [Column(name:"warehouse_id")]
        public long WarehouseId { get; set; }
    }
}