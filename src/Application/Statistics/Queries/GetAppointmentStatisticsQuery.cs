using DentalApp.Application.Common.Models;
using MediatR;

namespace DentalApp.Application.Statistics.Queries;

public class GetAppointmentStatisticsQuery : IRequest<List<AppointmentStatisticsDto>>
{
}
