using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MechMate.Services
{
    public class VinLookupService
    {
        private readonly HttpClient _httpClient;

        public VinLookupService(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<VinDecodeResult?> LookupVinAsync(string vin)
        {
            var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{vin}?format=json";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<VinDecodeResult>(json);
        }
    }

    public class VinDecodeResult
    {
        public string? Message { get; set; }
        public VinResult[]? Results { get; set; }
    }

    public class VinResult
    {
        public string? Variable { get; set; }
        public string? Value { get; set; }
    }
}