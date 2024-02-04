namespace Api.Models.CSV
{
    public class CsvFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public DateTime UploadedTime { get; set; }
    }
}
