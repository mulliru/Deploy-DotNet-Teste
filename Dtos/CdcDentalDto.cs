using System.Text.Json.Serialization;

namespace Sprint03.Dtos
{
    public class CdcDentalDto
    {
        [JsonPropertyName("year")]
        public string? Year { get; set; }

        [JsonPropertyName("locationabbr")]
        public string? LocationAbbr { get; set; }

        [JsonPropertyName("locationdesc")]
        public string? LocationDesc { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("indicator")]
        public string? Indicator { get; set; }

        [JsonPropertyName("response")]
        public string? Response { get; set; }

        [JsonPropertyName("datasource")]
        public string? DataSource { get; set; }

        [JsonPropertyName("data_value_unit")]
        public string? DataValueUnit { get; set; }

        [JsonPropertyName("data_value_type")]
        public string? DataValueType { get; set; }

        [JsonPropertyName("data_value")]
        public string? DataValue { get; set; }

        [JsonPropertyName("high_confidence_interval")]
        public string? HighConfidenceInterval { get; set; }

        [JsonPropertyName("low_confidence_interval")]
        public string? LowConfidenceInterval { get; set; }

        [JsonPropertyName("samplesize")]
        public string? SampleSize { get; set; }

        [JsonPropertyName("break_out")]
        public string? BreakOut { get; set; }

        [JsonPropertyName("break_out_category")]
        public string? BreakOutCategory { get; set; }
    }
}
