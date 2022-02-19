using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using QMarket.Api.Interfaces;
using QMarket.Api.Models;
using QMarket.Api.ViewModels;
using ZhetistikApp.Api.DataAccess;

namespace QMarket.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly DapperContext _context;

        public OrderController(DapperContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<OrderViewModel>> GetOrdersAsync()
        {
            string sql = "select o.order_id, cus.customer_id, o.order_date, o.expected_date, oloc.location_id_from, oloc.location_id_to, count(p.product_id)as number_of_products, sum(p.list_price) as product_sum, sum(o.expected_delivery_price) as delivery_price from sales.orders as o ,  sales.customers as cus,  sales.order_items as oi,  production.products as p, map.locations as loc, map.ordered_locations as oloc where o.customer_id = cus.customer_id and oi.order_id = o.order_id and oi.product_id = p.product_id and oloc.order_id = o.order_id group by o.order_id, o.order_date, cus.customer_id, o.expected_date, oloc.location_id_from, oloc.location_id_to order by o.order_id";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(sql, (SqlConnection)connection);
                var reader = await command.ExecuteReaderAsync();
                var list = new List<OrderViewModel>();
                while(await reader.ReadAsync())
                {
                    var model = new OrderViewModel();
                    model.OrderId = reader.GetInt32(0);
                    model.CustomerId = reader.GetInt32(1);
                    model.OrderDate = reader.GetDateTime(2);
                    model.ExpectedDate = reader.GetDateTime(3);
                    model.LocationIdFrom = reader.GetInt32(4);
                    model.LocationIdTo = reader.GetInt32(5);
                    model.NumberOfProducts = reader.GetInt32(6);
                    model.ProductSum = reader.GetInt32(7);
                    model.DeliveryPrice = reader.GetDecimal(8);
                    list.Add(model);
                }
                return list;
            }
        }
    }
}