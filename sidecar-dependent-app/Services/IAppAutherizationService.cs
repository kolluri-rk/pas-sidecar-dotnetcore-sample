using System.Collections.Generic;
using System.Threading.Tasks;

namespace sidecar_dependent_app.Services
{
    public interface IAppAutherizationService
    {
        Task<IEnumerable<string>> GetAthenticatedUserScopesAsync(long appId, long userId);
    }
}