using Featurize;
using Featurize.AspNetCore;
using Featurize.Repositories;
using Inkomen.Features.Inkomens;

namespace Inkomen.Features;

public class InkomenFeature : IWebApplicationFeature,
    IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(IServiceCollection services)
    {

    }

    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<VerzamelInkomen, Guid>();
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/v1/inkomen/");

        group.MapGet("/{id}", GetVerzamelInkomen.HandleAsync);
        group.MapPost("/", CreateInkomen.HandleAsync);
        group.MapPost("/loondienst", LoondienstToevoegen.HandleAsync);
    }
}
