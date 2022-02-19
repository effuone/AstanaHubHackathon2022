using System.Data.SqlClient;
using QMarket.Api.Interfaces;
using QMarket.Api.Models;
using ZhetistikApp.Api.DataAccess;

namespace QMarket.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _context;

        public CustomerRepository(DapperContext dapper)
        {
            _context = dapper;
        }

        public async Task<int> CreateAsync(Customer model)
        {
            string sql = @"INSERT INTO sales.customers (first_name, last_name) VALUES (@firstName, @lastName) SET @customer_id = SCOPE_IDENTITY();";
            using(var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(sql, (SqlConnection) connection);
                command.Parameters.Add(new SqlParameter("@firstName", model.FirstName));
                command.Parameters.Add(new SqlParameter("@lastName", model.LastName));

                var output = new SqlParameter
                {
                    ParameterName = "@customer_id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };
                command.Parameters.Add(output);
                await command.ExecuteNonQueryAsync();
                return (int)output.Value;
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var query = $"SELECT c.customer_id, c.first_name, c.last_name FROM sales.customers as c";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(query, (SqlConnection)connection);
                var reader = await command.ExecuteReaderAsync();
                var list = new List<Customer>();
                while(await reader.ReadAsync())
                {
                    var customer = new Customer();
                    customer.CustomerId = reader.GetInt32(0);
                    customer.FirstName = reader.GetString(1);
                    customer.LastName = reader.GetString(2);
                    list.Add(customer);
                }
                return list;
            }
        }

        public async Task<Customer> GetAsync(int id)
        {
            string sql = @"SELECT c.customer_id, c.first_name, c.last_name FROM sales.customers as c WHERE c.customer_id = @id";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(sql, (SqlConnection)connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                var reader = await command.ExecuteReaderAsync();
                if (!reader.HasRows)
                {
                    return null;
                }
                await reader.ReadAsync();
                var model = new Customer();
                model.CustomerId = reader.GetInt32(0);
                model.FirstName = reader.GetString(1);
                model.LastName = reader.GetString(2);
                return model;
            }
        }

        public Task UpdateAsync(int id, Customer model)
        {
            throw new NotImplementedException();
        }
    }
}