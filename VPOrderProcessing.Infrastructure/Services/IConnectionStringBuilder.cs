using VPOrderProcessor.Infrastructure.Options;

namespace VPOrderProcessor.Infrastructure.Services
{
    public interface IConnectionStringBuilder
    {
        string GiveConnectionString(ConnectionStringOptions connectionStringOptions);
    }
}
