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

        public async Task<List<DetailsRentalDto>?> UpdateRentalStatusDto(UpdateRentalStatusDto dto)
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

    }
}