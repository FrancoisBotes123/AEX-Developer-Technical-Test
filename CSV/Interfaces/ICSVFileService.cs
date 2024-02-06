using CSV.Models.CSV;

namespace CSV.Interfaces
{
    public interface ICSVFileService
    {
        Task<List<CsvFileDto>> GetCSVFilesAsync();
        Task DeleteCSVFileAsync(int id);
        Task UpdateCSVFileAsync(CsvFileDto csvFile);
        Task<CsvFile> GetCSVFileByIdAsync(int id);
    }
}
