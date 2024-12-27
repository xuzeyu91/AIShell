

using AIShell.Doamin.Common.DependencyInjection;
using AIShell.Doamin.Repositories.Base;

namespace AIShell.Domain.Repositories
{
    [ServiceDescription(typeof(ISessions_Repositories), ServiceLifetime.Scoped)]
    public class Sessions_Repositories : Repository<Sessions>, ISessions_Repositories
    {
    }
}
