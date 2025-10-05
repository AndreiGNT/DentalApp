﻿using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Constants;
using DentalApp.Domain.Entities;
using MediatR;

namespace DentalApp.Application.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand(
        string UserId,
        int DoctorId,
        int ProcedureId,
        DateTime StartTime,
        DateTime EndTime
    ) : IRequest<int>;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Appointment
        {
            UserId = request.UserId,
            DoctorId = request.DoctorId,
            ProcedureId = request.ProcedureId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = AppointmentStatus.Pending
        };

        _context.Appointments.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
