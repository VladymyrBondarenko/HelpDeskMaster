using Microsoft.Data.SqlClient;
using Npgsql;

namespace HelpDeskMaster.Persistence.Data
{
    public interface IDbConnectionFactory
    {
        Task<NpgsqlConnection> GetOpenConnection(CancellationToken cancellationToken = default);
    }
}