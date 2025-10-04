using DentalApp.Domain.Entities;

namespace DentalApp.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Appointment, LookupDto>();
            CreateMap<User, LookupDto>();
        }
    }
}
