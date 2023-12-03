using System.Data.SqlClient;
using VPOrderProcessor.Infrastructure.Options;

namespace VPOrderProcessor.Infrastructure.Services
{
    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        public string GiveConnectionString(ConnectionStringOptions connectionStringOptions)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = connectionStringOptions.Server,
                InitialCatalog = connectionStringOptions.Database,
                ApplicationName = connectionStringOptions.ApplicationName
            };

            if (string.IsNullOrWhiteSpace(connectionStringOptions.UserId))
            {
                connectionStringBuilder.IntegratedSecurity = true;
            }
            else
            {
                connectionStringBuilder.UserID = connectionStringOptions.UserId;
                connectionStringBuilder.Password = connectionStringOptions.Password;
            }

            return connectionStringBuilder.ToString();
        }
    }
}
