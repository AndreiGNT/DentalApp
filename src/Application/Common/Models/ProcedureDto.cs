using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalApp.Application.Common.Models;
public class ProcedureDto
{
    public int Id { get; set; }

    public string ProcedureName { get; set; } = string.Empty;

    public TimeSpan Duration { get; set; }

    public int Price { get; set; }
}
