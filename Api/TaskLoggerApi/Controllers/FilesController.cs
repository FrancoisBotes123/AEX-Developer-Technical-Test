using Api.Interfaces;
using Api.Models.CSV;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskLoggerApi.Controllers;

namespace Api.Controllers
{
    public class FilesController : BaseApiController
    {
        private readonly ICsvRepository _csvRepository;
        private readonly IMapper _mapper;
        public FilesController(ICsvRepository csvRepository, IMapper mapper)
        {
            _csvRepository = csvRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CsvFileDto>>> GetAllCsvFiles()
        {
            var csvFiles = await _csvRepository.GetAllCsvFilesAsync();

            var csvFilesToReturn = _mapper.Map<IEnumerable<CsvFileDto>>(csvFiles);

            return Ok(csvFilesToReturn);
        }

        [HttpPost("csv-upload")]
        public async Task<IActionResult> UploadCsvFile([FromForm] IFormFile csvFile)
        {

            if (csvFile.Length > 0)
            {
                await _csvRepository.SaveCsvAsync(csvFile);
                return Ok("File uploaded successfully.");
            }
            else
            {
                return BadRequest("File is empty.");
            }
        }

        [HttpPut("csv-update/{id}")]
        public async Task<IActionResult> UpdateCsvFile(int id, [FromForm] IFormFile csvFile)
        {
            if (csvFile.Length > 0)
            {
                await _csvRepository.UpdateCsvAsync(id, csvFile);
                return Ok("File updated successfully.");
            }
            else
            {
                return BadRequest("File is empty.");
            }
        }

        [HttpDelete("csv-delete/{id}")]
        public async Task<IActionResult> DeleteCsvFile(int id)
        {
            try
            {
                await _csvRepository.DeleteCsvAsync(id);
                return Ok("File deleted successfully.");
            }
            catch (FileNotFoundException)
            {
                return NotFound("CSV file not found.");
            }
        }
    }
}
