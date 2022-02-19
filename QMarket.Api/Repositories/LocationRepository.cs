using System.Data.SqlClient;
using QMarket.Api.Interfaces;
using QMarket.Api.Models;
using ZhetistikApp.Api.DataAccess;

namespace QMarket.Api.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DapperContext _context;

        public LocationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Location model)
        {
            string sql = @"INSERT INTO map.locations (location_name, x_cord, y_cord, rsid) VALUES (@locationName, @x, @y, @rsid) SET @location_id = SCOPE_IDENTITY();";
            using(var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(sql, (SqlConnection) connection);
                command.Parameters.Add(new SqlParameter("@locationName", model.LocationId));
                command.Parameters.Add(new SqlParameter("@x", model.XCord));
                command.Parameters.Add(new SqlParameter("@y", model.YCord));
                command.Parameters.Add(new SqlParameter("@rsid", model.Rsid));

                var output = new SqlParameter
                {
                    ParameterName = "@location_id",
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

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var query = $"SELECT* FROM maps.locations as loc";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(query, (SqlConnection)connection);
                var reader = await command.ExecuteReaderAsync();
                var list = new List<Location>();
                while(await reader.ReadAsync())
                {
                    var model = new Location();
                    model.LocationId = reader.GetInt32(0);
                    model.LocationName = reader.GetString(1);
                    model.XCord = reader.GetDecimal(2);
                    model.YCord = reader.GetDecimal(3);
                    model.Rsid = reader.GetInt32(4);
                    list.Add(model);
                }
                return list;
            }
        }

        public async Task<Location> GetAsync(int id)
        {
            string sql = @"SELECT* FROM maps.locations as loc WHERE loc.location_id = @id";
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
                var model = new Location();
                model.LocationId = reader.GetInt32(0);
                model.LocationName = reader.GetString(1);
                model.XCord = reader.GetDecimal(2);
                model.YCord = reader.GetDecimal(3);
                model.Rsid = reader.GetInt32(4);
                return model;
            }
        }

        public Task UpdateAsync(int id, Location model)
        {
            throw new NotImplementedException();
        }
    }
}