
using Application.Procedures.Commands.CreateProcedure;
using Application.Procedures.Commands.DeleteProcedure;
using Application.Procedures.Commands.UpdateProcedure;
using Application.Procedures.Queries.GetProcedureById;
using Application.Procedures.Queries.GetProcedures;

namespace DentalApp.Web.Endpoints.Procedures;

public static class ProceduresEndpoints
{
    public static IEndpointRouteBuilder MapProceduresEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/procedures")
                          .WithTags("Procedures");

        // GET /api/procedures
        group.MapGet("/", async (ISender sender, CancellationToken ct) =>
        {
            var list = await sender.Send(new GetProcedures(), ct);

            var response = list.Select(p =>
                new ProcedureResponse(p.Id, p.ProcedureName, p.Duration, p.Price)).ToList();

            return Results.Ok(response);
        });

        // GET /api/procedures/{id}
        group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken ct) =>
        {
            var dto = await sender.Send(new GetProcedureById { Id = id }, ct);
            var response = new ProcedureResponse(dto.Id, dto.ProcedureName, dto.Duration, dto.Price);
            return Results.Ok(response);
        });

        // POST /api/procedures  (Admin only)
        group.MapPost("/", async (CreateProcedureRequest req, ISender sender, CancellationToken ct) =>
        {
            await sender.Send(new CreateProcedure
            {
                ProcedureName = req.ProcedureName,
                Duration = req.Duration,
                Price = req.Price
            }, ct);

            return Results.Ok();
        })
        .RequireAuthorization("Admin");


        // PUT /api/procedures/{id}  (Admin only)
        group.MapPut("/{id:int}", async (int id, UpdateProcedureRequest req, ISender sender, CancellationToken ct) =>
        {
            await sender.Send(new UpdateProcedure
            {
                Id = id,
                ProcedureName = req.ProcedureName,
                Duration = req.Duration,
                Price = req.Price
            }, ct);

            return Results.Ok();
        })
        .RequireAuthorization("Admin");


        // DELETE /api/procedures/{id}  (Admin only)
        group.MapDelete("/{id:int}", async (int id, ISender sender, CancellationToken ct) =>
        {
            await sender.Send(new DeleteProcedure { Id = id }, ct);
            return Results.NoContent();
        })
        .RequireAuthorization("Admin");

        return routes;
    }
}
