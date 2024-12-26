
using AIShell.Doamin.Common.DependencyInjection;
using AIShell.Doamin.Repositories.Base;

namespace AIShell.Domain.Repositories
{
    [ServiceDescription(typeof(IAgents_Repositories), ServiceLifetime.Scoped)]
    public class Agents_Repositories : Repository<Agents>, IAgents_Repositories
    {
    }
}
