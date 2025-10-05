﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DentalApp.Domain.Constants;

namespace DentalApp.Domain.Entities;
public class Appointment : BaseAuditableEntity
{
    [ForeignKey(nameof(User))]
    public string? UserId { get; set; }
    
    public ApplicationUser? User { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }

    public Doctor? Doctor { get; set; }


    [ForeignKey(nameof(Procedure))]
    public int ProcedureId { get; set; }

    public Procedure? Procedure { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Status { get; set; } = AppointmentStatus.Pending;
}
