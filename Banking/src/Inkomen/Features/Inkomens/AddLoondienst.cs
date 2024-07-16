namespace Inkomen.Features.Inkomens;

public static class LoondienstToevoegen
{
    public static async Task<Results<Ok, NotFound>> HandleAsync(
        [FromServices] AggregateManager<VerzamelInkomen, Guid> vManger,
        [FromServices] AggregateManager<Loondienst, Guid> lManager,
        [FromBody] LoondienstToevoegenRequest request)
    {
        var verzamelinkomen = await vManger.LoadAsync(request.VerzamelInkomenId);
        var loondienst = await lManager.LoadAsync(request.LoondienstId);

        if (verzamelinkomen is null || loondienst is null)
        {
            return TypedResults.NotFound();
        }

        verzamelinkomen.AddInkomen(loondienst);

        await vManger.SaveAsync(verzamelinkomen);

        return TypedResults.Ok();
    }

    public record LoondienstToevoegenRequest(Guid VerzamelInkomenId, Guid LoondienstId);
}
