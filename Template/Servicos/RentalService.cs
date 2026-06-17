using Microsoft.EntityFrameworkCore;
using Retail.API.Servicos;
using Template.Data;
using Template.DTO;
using Template.Entities;
using Template.Enums;
using Template.Mappers;

namespace Template.Services
{
    internal class RentalService : IRentalService
    {
        private readonly DataContext _dataContext;

        private readonly IClientServiceCaller _clientServiceCaller;
        private readonly IVehicleServiceCaller _vehicleServiceCaller;


        public RentalService(
            IClientServiceCaller clientServiceCaller,
            IVehicleServiceCaller vehicleServiceCaller,
            DataContext dataContext)
        {
            _dataContext = dataContext;
            _clientServiceCaller = clientServiceCaller;
            _vehicleServiceCaller = vehicleServiceCaller;
        }

        public async Task<List<DetailsRentalDto>> GetAllAsync()
        {
            var rentals = await _dataContext.Rentals.ToListAsync();

            return rentals
                .Select(RentalMapper.ToDetailsDto)
                .ToList();
        }

        public async Task<DetailsRentalDto> CreateAsync(CreateRentalDto rentalDto)
        {
            bool clientExists;
            bool vehicleExists;

            try
            {
                clientExists = await _clientServiceCaller.ClientExists(rentalDto.ClientId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Client service unavailable", ex);
            }

            if (!clientExists)
                throw new InvalidOperationException("Client not found");

            try
            {
                vehicleExists = await _vehicleServiceCaller.VehicleExists(rentalDto.VehicleId);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Vehicle service unavailable");
            }

            if (!vehicleExists)
                throw new InvalidOperationException("Vehicle not found");

            Rental rental = RentalMapper.ToEntity(rentalDto);

            _dataContext.Rentals.Add(rental);
            await _dataContext.SaveChangesAsync();

            return RentalMapper.ToDetailsDto(rental);
        }

        public async Task<List<DetailsRentalDto>?> UpdateRentalStatus(UpdateRentalStatusDto dto)
        {
            var rentals = await _dataContext.Rentals
                .Where(x => x.ClientId == dto.ClientId && x.ReturnDate == null)
                .ToListAsync();

            if (rentals == null || rentals.Count == 0) return null;

            foreach (var rental in rentals)
            {
                rental.Status = RentalStatus.Cancelled;
            }

            await _dataContext.SaveChangesAsync();

            return rentals.Select(RentalMapper.ToDetailsDto).ToList();
        }

        public async Task<List<DetailsRentalDto>?> UpdateRentalByVehicleStatus(UpdateVehicleStatusDto dto)
        {
            var rentals = await _dataContext.Rentals
                .Where(x => x.VehicleId == dto.VehicleId && x.ReturnDate == null)
                .ToListAsync();

            if (rentals == null || rentals.Count == 0) return null;

            foreach (var rental in rentals)
            {
                rental.Status = RentalStatus.Cancelled;
            }

            await _dataContext.SaveChangesAsync();

            return rentals.Select(RentalMapper.ToDetailsDto).ToList();
        }
    }
}