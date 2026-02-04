using System.Text.Json.Serialization;

namespace CTM.Models.DTOs
{
    // DTO = Data Transfer Object
    public class EdbSchoolDto
    {
        // [JsonPropertyName("xxx")] used to map JSON property names to C# property names

        [JsonPropertyName("SCHOOL NO.")]
        public long SchoolNo { get; set; }

        [JsonPropertyName("ENGLISH NAME")]
        public string NameEn { get; set; } = "";
        
        [JsonPropertyName("中文名稱")]
        public string Name { get; set; } = "";
        
        [JsonPropertyName("DISTRICT")]
        public string DistrictEn { get; set; } = "";
        
        [JsonPropertyName("分區")]
        public string District { get; set; } = "";
        
        [JsonPropertyName("SCHOOL LEVEL")]
        public string SchoolLevel { get; set; } = "";
        
        [JsonPropertyName("學校類型")]
        public string SchoolLevelZh { get; set; } = "";
    }
}