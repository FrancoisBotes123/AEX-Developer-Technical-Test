﻿using CSV.Models.CSV;
using System.Data;


namespace CSV.Interfaces
{
    public interface ICSVFileService
    {
        Task<List<CsvFileDto>> GetCSVFilesAsync();
        Task<bool> UploadCSVFileAsync(string fileName, DataTable dataTable);
        Task DeleteCSVFileAsync(int id);
        Task<bool> UpdateCSVFileAsync(int id, DataTable dataTable);
        Task<CsvFile> GetCSVFileByIdAsync(int id);
    }
}
