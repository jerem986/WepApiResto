using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Security.Models
{
    public interface IPayload
    {
        string Identifier { get; }
        string Email { get; }
        IEnumerable<string> Roles { get; }
    }
}
