using System.Text.Json.Serialization;

namespace AdvancedWeatherForecast.Web.Models.OpenMeteo;

public sealed class OpenMeteoGeocodingResponse
{
    [JsonPropertyName("results")]
    public List<OpenMeteoLocationDto>? Results { get; set; }

    [JsonPropertyName("error")]
    public bool? Error { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}

public sealed class OpenMeteoLocationDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("elevation")]
    public double? Elevation { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("admin1")]
    public string? Admin1 { get; set; }

    [JsonPropertyName("population")]
    public int? Population { get; set; }

    public string DisplayName
    {
        get
        {
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                parts.Add(Name);
            }

            if (!string.IsNullOrWhiteSpace(Admin1))
            {
                parts.Add(Admin1);
            }

            if (!string.IsNullOrWhiteSpace(Country))
            {
                parts.Add(Country);
            }

            return string.Join(", ", parts);
        }
    }
}
