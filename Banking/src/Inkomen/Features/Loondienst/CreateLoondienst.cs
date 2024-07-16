using Microsoft.AspNetCore.Mvc;

namespace Inkomen.Features;

public class CreateLoondienst
{
    public static async Task<Ok<CreateResponse>> HandleAsync(
        [FromServices] AggregateManager<Loondienst, Guid> manager,
        [FromBody] CreateRequest request
        )
    {
        var inkomen = Loondienst.Create(request.Werknemer, request.Werkgever, request.Salaris, request.IngangsDatum);

        await manager.SaveAsync(inkomen);

        return TypedResults.Ok(new CreateResponse(inkomen.Id));
    }

    public record CreateRequest(Werkgever Werkgever, Werknemer Werknemer, Amount Salaris, DateOnly IngangsDatum);
    public record CreateResponse(Guid Id);
}
