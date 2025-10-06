﻿using DentalApp.Application.Common.Interfaces;
using DentalApp.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Application.Doctors.Queries;

public record GetDoctorByIdQuery : IRequest<DoctorDto?>
{
    public int Id { get; init; }
}

public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto?>
{
    private readonly IApplicationDbContext _context;

    public GetDoctorByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorDto?> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
                .Include(d => d.DoctorProcedures)
                .ThenInclude(dp => dp.Procedure)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (doctor == null)
            return null;

        return new DoctorDto
        {
            Id = doctor.Id,
            FullName = doctor.FullName,
            Procedures = doctor.DoctorProcedures
                .Where(dp => dp.Procedure != null)
                .Select(dp => dp.Procedure!.ProcedureName)
                .ToList()
        };
    }
}
