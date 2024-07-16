namespace Inkomen.Features.Inkomens;

public class GetVerzamelInkomen
{
    public static async Task<Ok<VerzamelInkomen>> HandleAsync(
        [FromServices] AggregateManager<VerzamelInkomen, Guid> manager,
        [FromRoute] Guid id
        )
    {
        var inkomen = await manager.LoadAsync(id);

        return TypedResults.Ok(inkomen);
    }

    public record GetVerzamelInkomenResponse();
    public record GetVerzamelInkomenRequest(Guid VerzamelinkomenId);
}
