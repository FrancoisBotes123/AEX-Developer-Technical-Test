using CSVv2.Models.CSV;

namespace CSVv2.Interfaces
{
    public interface ICSVFileService
    {
        Task<List<CsvFileDto>> GetCSVFilesAsync();
        Task DeleteCSVFileAsync(int id);
        Task UpdateCSVFileAsync(CsvFileDto csvFile);
        Task<CsvFileDto> GetCSVFileByIdAsync(int id);
    }
}
