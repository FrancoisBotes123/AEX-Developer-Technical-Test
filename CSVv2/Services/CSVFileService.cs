using CSVv2.Interfaces;
using CSVv2.Models.CSV;
using System.Net.Http;
using System.Net.Http.Json;

namespace CSV.Services
{
    public class CSVFileService : ICSVFileService
    {
        private readonly HttpClient _httpClient;

        public CSVFileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task DeleteCSVFileAsync(int id)
        {
            await _httpClient.DeleteAsync($"csv/delete/{id}");
        }

        public async Task<CsvFileDto> GetCSVFileByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CsvFileDto>($"api/files/csv/{id}");
            if (response != null)
            {
                return response;
            }
            // Handle the case where the file is not found
            throw new KeyNotFoundException("CSV file not found.");
        }

        public async Task UpdateCSVFileAsync(CsvFileDto csvFile)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/files/csv/update/{csvFile.Id}", csvFile);
            if (!response.IsSuccessStatusCode)
            {
                // Handle the error
            }
        }

        public async Task<List<CsvFileDto>> GetCSVFilesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CsvFileDto>>("api/files/csv");
            return response ?? new List<CsvFileDto>();
        }
    }
}
