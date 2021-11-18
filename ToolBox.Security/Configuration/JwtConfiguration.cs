using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Security.Configuration
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationDuration { get; set; }
        public string Signature { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public GoogleAuthSettings GoogleAuthSettings {  get; set; }
    }
    public class GoogleAuthSettings
    {
        public string ClientId { get; set; }
    }
}
