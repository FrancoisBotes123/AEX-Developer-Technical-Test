using Api.Interfaces;
using Api.Models.CSV;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskLoggerApi.Data;

namespace Api.Services
{
    public class CsvService : ICsvRepository
    {
        private readonly ApiDbContext _apiDbContext;
        


        public CsvService(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }
        public async Task<IEnumerable<CsvFile>> GetAllCsvFilesAsync()
        {
            return await _apiDbContext.CsvFiles.ToListAsync();
        }
        public async Task DeleteCsvAsync(int id)
        {
            var fileToDelete = await _apiDbContext.CsvFiles.FindAsync(id);
            if (fileToDelete != null)
            {
                _apiDbContext.CsvFiles.Remove(fileToDelete);
                await _apiDbContext.SaveChangesAsync();
            }
            else
            {
                throw new FileNotFoundException("CSV file not found.");
            }
        }

        public async Task SaveCsvAsync(IFormFile csvFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await csvFile.CopyToAsync(memoryStream);
                var file = new CsvFile
                {
                    FileName = csvFile.FileName,
                    Content = memoryStream.ToArray(),
                    UploadedTime = DateTime.UtcNow
                };

                _apiDbContext.CsvFiles.Add(file);
                await _apiDbContext.SaveChangesAsync();
                
            }
        }

        public async Task UpdateCsvAsync(int id, IFormFile csvFile)
        {
            var fileToUpdate = await _apiDbContext.CsvFiles.FindAsync(id);
            if (fileToUpdate == null)
            {
                throw new FileNotFoundException("CSV file not found.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await csvFile.CopyToAsync(memoryStream);
                fileToUpdate.Content = memoryStream.ToArray();
                fileToUpdate.FileName = csvFile.FileName;
                fileToUpdate.UploadedTime = DateTime.UtcNow;
                await _apiDbContext.SaveChangesAsync();
            }
        }
    }
}
