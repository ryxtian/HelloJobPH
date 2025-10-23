using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EmploymentType
    {
        FullTime,
        PartTime,
        Contract 
    }
}
