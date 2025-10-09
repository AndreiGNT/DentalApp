namespace DentalApp.Application.Common.Models;
public class ProcedureDto
{
    public int Id { get; set; }

    public string ProcedureName { get; set; } = string.Empty;

    public TimeSpan Duration { get; set; }

    public int Price { get; set; }

    public List<string> Doctors { get; set; } = new();
}
