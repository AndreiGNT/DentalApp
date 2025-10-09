﻿using DentalApp.Application.Procedures.Queries;
using DentalApp.Application.Procedures.Commands.UpdateProcedure;
using DentalApp.Application.Procedures.Commands.DeleteProcedure;

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
            var list = await sender.Send(new GetProceduresQuery(), ct);

            var response = list.Select(p =>
                new ProcedureResponse(p.Id, p.ProcedureName, p.Duration, p.Price)).ToList();

            return Results.Ok(response);
        });

        // GET /api/procedures/{id}
        group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken ct) =>
        {
            var dto = await sender.Send(new GetProcedureQuery { Id = id }, ct);
            var response = new ProcedureResponse(dto.Id, dto.ProcedureName, dto.Duration, dto.Price);
            return Results.Ok(response);
        });

        // POST /api/procedures  (Admin only)
        group.MapPost("/", async (CreateProcedureRequest req, ISender sender, CancellationToken ct) =>
        {
            await sender.Send(new CreateProcedureCommand
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
            await sender.Send(new UpdateProcedureCommand
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
            await sender.Send(new DeleteProcedureCommand { Id = id }, ct);
            return Results.NoContent();
        })
        .RequireAuthorization("Admin");

        return routes;
    }
}
