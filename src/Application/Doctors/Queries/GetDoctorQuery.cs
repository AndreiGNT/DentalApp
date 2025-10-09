using DentalApp.Application.Common.Models;

namespace DentalApp.Application.Doctors.Queries;

public record GetDoctorQuery : IRequest<DoctorDto?>
{
    public int Id { get; init; }
}
