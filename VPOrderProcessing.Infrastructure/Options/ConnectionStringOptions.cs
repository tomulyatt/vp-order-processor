using System.ComponentModel.DataAnnotations;

namespace VPOrderProcessor.Infrastructure.Options
{
    public class ConnectionStringOptions
    {
        public const string ConfigBinding = "SqlServerConnection";

        [Required(AllowEmptyStrings = false)]
        public string Server { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Database { get; set; }

        public string UserId { get; set; }
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ApplicationName { get; set; }
    }
}
