using Common.Storage;
using Common.ValueObjects;
using Featurize;
using Featurize.AspNetCore;
using Featurize.Repositories;
using Inkomen.Domain;
using Inkomen.Domain.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inkomen.Features;

public class LoondienstFeature : IWebApplicationFeature,
    IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<Loondienst, Guid>();
    }

    public void Configure(IServiceCollection services)
    {

    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/v1/loondienst");

        group.MapPost("/", CreateLoondienst.HandleAsync);
    }

    
}
