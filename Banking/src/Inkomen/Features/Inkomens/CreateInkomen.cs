
namespace Inkomen.Features;

public static class CreateInkomen
{
    public static async Task<Ok<Response>> HandleAsync(
        [FromServices] AggregateManager<VerzamelInkomen, Guid> manager,
        [FromBody] Request request
        )
    {
        var inkomen = VerzamelInkomen.Create();

        await manager.SaveAsync(inkomen);

        return TypedResults.Ok(new Response(inkomen.Id));
    }

    public record Request();
    public record Response(Guid InkomenId);
}


