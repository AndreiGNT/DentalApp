
namespace DentalApp.Application.Doctors.Commands.DeleteDoctor;

public record DeleteDoctorCommand : IRequest
{
    public int Id { get; init; }
}
