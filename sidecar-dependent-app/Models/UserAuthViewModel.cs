using System.Collections.Generic;

namespace sidecar_dependent_app.Models
{
    public class UserAuthViewModel
    {
        public IEnumerable<string> Scopes { get; set; }
    }
}