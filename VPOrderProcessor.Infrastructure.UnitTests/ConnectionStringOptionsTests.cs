using FluentAssertions;
using TestUtilities;
using VPOrderProcessor.Infrastructure.Options;

namespace VPOrderProcessor.Infrastructure.UnitTests
{
    public class ConnectionStringOptionsTests
    {
        [Fact]
        public void ShouldFailModelValidation_GivenConnectionOptions_WithoutServer()
        {
            ConnectionStringOptions connectionStringOptions = new ConnectionStringOptions()
            {
                ApplicationName = "app",
                Database = "Test"
            };

            bool isValid = connectionStringOptions.IsValid();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void ShouldFailModelValidation_GivenConnectionOptions_WithoutDatabase()
        {
            ConnectionStringOptions connectionStringOptions = new ConnectionStringOptions()
            {
                ApplicationName = "app",
                Server = "Test"
            };

            bool isValid = connectionStringOptions.IsValid();

            isValid.Should().BeFalse();
        }
    }
}
