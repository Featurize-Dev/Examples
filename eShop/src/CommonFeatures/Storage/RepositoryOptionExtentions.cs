using Featurize.Repositories;
using Featurize.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFeatures.Storage;
public static class RepositoryProviderOptionsExtensions
{
    public static RepositoryProviderOptions AddAggregate<TAggregate, TId>(this RepositoryProviderOptions options, Action<RepositoryOptions> o)
    {
        var aggregate = typeof(TAggregate).Name;
        options.AddRepository<PersistendEvent<TId>, Guid>(o);

        return options;
    }

}
