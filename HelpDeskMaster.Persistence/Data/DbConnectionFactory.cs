using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HelpDeskMaster.Persistence.Data
{
    internal sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<NpgsqlConnection> GetOpenConnection(CancellationToken cancellationToken = default)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("HdmDbConnection"));
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
    }
}
