using AutoMapper;
using DentalApp.Application.Appointments.Commands.CreateAppointment;
using DentalApp.Application.Appointments.Commands.UpdateAppointment;
using DentalApp.Application.Common.Models;
using DentalApp.Domain.Entities;

namespace DentalApp.Application.Appointments.Mapping
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            // Mapare entitate → DTO
            //CreateMap<Appointment, AppointmentDto>()
            //    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
            //    .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
            //    .ForMember(dest => dest.ProcedureName, opt => opt.MapFrom(src => src.Procedure.ProcedureName));

            // Mapare DTO → entitate (dacă e cazul)
            CreateMap<AppointmentDto, Appointment>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.Procedure, opt => opt.Ignore());

            // Mapare command → entitate (pentru Create/Update)
            CreateMap<CreateAppointmentCommand, Appointment>();
            CreateMap<UpdateAppointmentCommand, Appointment>();
        }
    }
}

