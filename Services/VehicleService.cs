using Sigorta.DTOs;
using SigortaApp.Models;
using SigortaApp.Repositories.Interfaces;
using SigortaApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SigortaApp.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repository;

        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<object>> GetVehiclesAsync(
            string? make,
            string? model,
            string? plateStart,
            string? plateEnd,
            int? id)
        {
            var vehicles = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(make))
                vehicles = vehicles.Where(v => v.Make.ToLower() == make.ToLower()).ToList();

            if (!string.IsNullOrEmpty(model))
                vehicles = vehicles.Where(v => v.Model.ToLower() == model.ToLower()).ToList();

            if (id.HasValue)
                vehicles = vehicles.Where(v => v.Id == id).ToList();

            var withLetters = vehicles.Select(v => new
            {
                v.Id,
                v.Plate,
                v.Make,
                v.Model,
                v.CreatedAt,
                Letters = ExtractLetters(v.Plate)
            });

            if (!string.IsNullOrEmpty(plateStart))
                withLetters = withLetters.Where(v => v.Letters.StartsWith(plateStart.ToUpper()));

            if (!string.IsNullOrEmpty(plateEnd))
                withLetters = withLetters.Where(v => v.Letters.EndsWith(plateEnd.ToUpper()));

            return withLetters
                .OrderBy(v => v.Letters)
                .Select(v => new
                {
                    v.Id,
                    v.Plate,
                    v.Make,
                    v.Model,
                    v.CreatedAt
                });
        }

        public async Task<object> AddVehicleAsync(VehicleDTO input)
        {
            var vehicle = new Vehicle
            {
                Id = input.Id,
                Plate = input.Plate,
                Make = input.Make,
                Model = input.Model,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(vehicle);
            await _repository.SaveChangesAsync();

            return vehicle;
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _repository.GetByIdAsync(id);
            if (vehicle == null)
                return false;

            await _repository.DeleteAsync(vehicle);
            await _repository.SaveChangesAsync();
            return true;
        }

        private static string ExtractLetters(string plate)
        {
            return new string(plate.Where(char.IsLetter).ToArray()).ToUpper();
        }
    }
}
