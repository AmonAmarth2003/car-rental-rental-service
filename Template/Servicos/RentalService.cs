using Microsoft.EntityFrameworkCore;
using Template.Data;
using Template.DTO;
using Template.Entities;
using Template.Enums;
using Template.Mappers;

namespace Template.Services
{
    public class RentalService : IRentalService
    {
        private readonly DataContext _dataContext;

        public RentalService(DataContext dataContext)
        {
            _dataContext = dataContext;
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
            Rental rental = RentalMapper.ToEntity(rentalDto);

            _dataContext.Rentals.Add(rental);
            await _dataContext.SaveChangesAsync();

            return RentalMapper.ToDetailsDto(rental);
        }

        public async Task<DetailsRentalDto?> UpdateStatusAsync(int id, RentalStatus status)
        {
            var rental = await _dataContext.Rentals.FindAsync(id);
            if (rental == null) return null;

            rental.Status = status;

            await _dataContext.SaveChangesAsync();
            return RentalMapper.ToDetailsDto(rental);
        }
    }
}