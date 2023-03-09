using System.Text.Json.Serialization;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.Student
{
    public class ProgramReadModel
    {
        public string? Code { get; set; }

        public string Name { get; set; } = null!;

        public string SectionName { get; set; } = null!;
        [JsonRequired]
        [JsonPropertyName("GradeType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GradeType GradingType { get; set; }
    }
}
