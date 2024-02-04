using Api.Interfaces;
using Api.Models.Vehicle;

namespace Api.Services
{
    public class VehicleService : IVehicleRepository
    {
        public Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVehicleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVehicleAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task UploadCsvFileAsync(Stream csvFileStream)
        {
            throw new NotImplementedException();
        }
    }
}
