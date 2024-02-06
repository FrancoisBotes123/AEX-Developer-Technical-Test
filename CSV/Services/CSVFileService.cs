using CSV.Interfaces;
using CSV.Models.CSV;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

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

        public async Task<CsvFile> GetCSVFileByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CsvFile>($"api/files/csv/{id}");
            if (response != null)
            {
                return response;
            }
            // Handle the case where the file is not found
            throw new KeyNotFoundException("CSV file not found.");
        }

        public async Task<bool> UpdateCSVFileAsync(int id, DataTable dataTable)
        {
            // Convert DataTable to CSV string
            var csvContent = ConvertDataTableToCsv(dataTable);
            using var stringContent = new StringContent(csvContent);
            using var formData = new MultipartFormDataContent();
            formData.Add(stringContent, "csvFile");

            var response = await _httpClient.PutAsync($"api/files/csv/update/{id}", formData);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CsvFileDto>> GetCSVFilesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CsvFileDto>>("api/files/csv");
            return response ?? new List<CsvFileDto>();
        }

        private string ConvertDataTableToCsv(DataTable dataTable)
        {
            var sb = new StringBuilder();

            // Column headers
            string[] columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            // Rows
            foreach (DataRow row in dataTable.Rows)
            {
                string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }
    }
}
