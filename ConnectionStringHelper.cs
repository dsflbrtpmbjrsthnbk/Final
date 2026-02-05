using Npgsql;

namespace UserManagementApp
{
    public static class ConnectionStringHelper
    {
        public static string BuildPostgresConnectionString(string? rawConnectionString)
        {
            if (string.IsNullOrWhiteSpace(rawConnectionString))
            {
                throw new InvalidOperationException(
                    "Connection string is empty. Set DATABASE_URL or ConnectionStrings:DefaultConnection.");
            }

            var input = rawConnectionString.Trim();

            if (!input.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) &&
                !input.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
            {
                return input;
            }

            var uri = new Uri(input);
            var userInfo = uri.UserInfo.Split(':', 2);

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = uri.Host,
                Port = uri.Port > 0 ? uri.Port : 5432,
                Database = uri.AbsolutePath.Trim('/'),
                Username = Uri.UnescapeDataString(userInfo[0]),
                Password = Uri.UnescapeDataString(userInfo[1]),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return builder.ConnectionString;
        }
    }
}
