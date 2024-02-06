using CSV.Interfaces;
using CSV.Models.CSV;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
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
            // Convert the CSV string to a Stream
            var csvStream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));
            // Create a StreamContent for the file upload
            using var fileContent = new StreamContent(csvStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "csvFile",
                FileName = $"updated_{id}.csv"
            };

            // Create MultipartFormDataContent and add the file content
            using var formData = new MultipartFormDataContent();
            formData.Add(fileContent, "csvFile", $"updated_{id}.csv");

            // Send the request to the server endpoint
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

        public async Task<bool> UploadCSVFileAsync(string fileName,DataTable dataTable)
        {
            // Convert DataTable to CSV string
            var csvContent = ConvertDataTableToCsv(dataTable);
            // Convert the CSV string to a Stream
            var csvStream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));
            // Create a StreamContent for the file upload
            using var fileContent = new StreamContent(csvStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "csvFile",
                FileName = $"{fileName}.csv" // Since this is a new file, you can set a default name or generate one dynamically
            };

            // Create MultipartFormDataContent and add the file content
            using var formData = new MultipartFormDataContent();
            formData.Add(fileContent, "csvFile", $"{fileName}.csv");

            // Send the request to the server endpoint
            var response = await _httpClient.PostAsync("api/files/csv/upload", formData);
            return response.IsSuccessStatusCode;
        }
    }
}
