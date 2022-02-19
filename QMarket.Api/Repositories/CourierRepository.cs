using System.Data.SqlClient;
using QMarket.Api.Interfaces;
using QMarket.Api.Models;
using ZhetistikApp.Api.DataAccess;

namespace QMarket.Api.Repositories
{
    public class CourierRepository : ICourierRepository
    {
        private readonly DapperContext _context;

        public CourierRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Courier model)
        {
            string sql = @"INSERT INTO sales.couriers (company_name) VALUES (@company_name) SET @courier_id = SCOPE_IDENTITY();";
            using(var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(sql, (SqlConnection) connection);
                command.Parameters.Add(new SqlParameter("@company_name", model.CompanyName));

                var output = new SqlParameter
                {
                    ParameterName = "@courier_id",
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

        public async Task<IEnumerable<Courier>> GetAllAsync()
        {
            var query = $"SELECT c.courier_id, c.company_name FROM sales.couriers as c";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(query, (SqlConnection)connection);
                var reader = await command.ExecuteReaderAsync();
                var list = new List<Courier>();
                while(await reader.ReadAsync())
                {
                    var model = new Courier();
                    model.CourierId = reader.GetInt32(0);
                    model.CompanyName = reader.GetString(1);
                    list.Add(model);
                }
                return list;
            }
        }

        public async Task<Courier> GetAsync(int id)
        {
            string sql = @"SELECT c.courier_id, c.company_name FROM sales.customers as c WHERE c.courier_id = @id";
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
                var model = new Courier();
                model.CourierId = reader.GetInt32(0);
                model.CompanyName = reader.GetString(1);
                return model;
            }
        }

        public Task UpdateAsync(int id, Courier model)
        {
            throw new NotImplementedException();
        }
    }
}