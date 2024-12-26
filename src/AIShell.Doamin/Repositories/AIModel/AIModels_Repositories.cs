

using AIShell.Doamin.Common.DependencyInjection;
using AIShell.Doamin.Repositories.Base;

namespace AIShell.Domain.Repositories
{
    [ServiceDescription(typeof(IAIModels_Repositories), ServiceLifetime.Scoped)]
    public class AIModels_Repositories : Repository<AIModels>, IAIModels_Repositories
    {
    }
}
