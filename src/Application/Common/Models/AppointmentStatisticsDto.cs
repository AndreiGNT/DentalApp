
namespace DentalApp.Application.Common.Models;
public class AppointmentStatisticsDto
{
    public string DoctorName { get; set; } = string.Empty;
    public string ProcedureName { get; set; } = string.Empty;
    public int AppointmentCount { get; set; }
}
