using FluentAssertions;
using VPOrderProcessor.Infrastructure.Options;
using VPOrderProcessor.Infrastructure.Services;

namespace VPOrderProcessor.Infrastructure.UnitTests
{
    public class ConnectionStringBuilderTests
    {
        private IConnectionStringBuilder? _connectionStringBuilder;


        [Fact]
        public void ShouldReturnValidConnectionString_GivenValidOptions_WithNoUserInformation()
        {
            _connectionStringBuilder = new ConnectionStringBuilder();

            ConnectionStringOptions connectionStringOptions = new ConnectionStringOptions()
            {
                ApplicationName = "app",
                Database = "Test",
                Server = "Test"
            };

            string connectionString = _connectionStringBuilder.GiveConnectionString(connectionStringOptions);

            connectionString.Should().NotBeNull().And.Contain("Integrated Security=True;");
        }

        [Fact]
        public void ShouldReturnValidConnectionString_GivenValidOptions_WithUserInformation()
        {
            _connectionStringBuilder = new ConnectionStringBuilder();

            ConnectionStringOptions connectionStringOptions = new ConnectionStringOptions()
            {
                ApplicationName = "app",
                Database = "Test",
                Server = "Test",
                UserId = "user",
                Password = "password"
            };

            string connectionString = _connectionStringBuilder.GiveConnectionString(connectionStringOptions);

            connectionString.Should().NotBeNull().And.NotContain("Integrated Security=True;");
        }
    }
}