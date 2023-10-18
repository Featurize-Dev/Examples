using Featurize.AspNetCore;
using Featurize.Repositories;
using Featurize.Repositories.MongoDB;
using Featurize.Todo.Features.Todo;
using Featurize.Todo.Features.Todo.ValueObjects;

namespace Featurize.Todo.Features.Storage;

public class StorageFeature : IWebApplicationFeature, IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(IServiceCollection services)
    {
        
        services.AddScoped<AggregateManager<Todos, TodoId>>();
    }

    public void Configure(RepositoryProviderOptions options)
    {
        options.AddRepository<PersistendEvent<TodoId>, Guid>(o =>
        {
            o.Provider(MongoRepositoryProvider.DefaultName);
            o.Database("Todos");
            o.CollectionName("todo-events");
        });
    }

    public void Use(WebApplication app)
    {
        
    }
}
