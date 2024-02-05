using Api.Models.CSV;
using Microsoft.AspNetCore.Mvc;

namespace Api.Interfaces
{
    public interface ICsvRepository
    {
        Task SaveCsvAsync(IFormFile csvFile);
        Task UpdateCsvAsync(int id, IFormFile csvFile);
        Task DeleteCsvAsync(int id);
        Task<CsvFile> GetCsvFileAsync(int id);
        Task<IEnumerable<CsvFile>> GetAllCsvFilesAsync();
    }
}
