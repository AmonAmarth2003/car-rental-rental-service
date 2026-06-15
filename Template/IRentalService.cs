using Template.DTO;
using Template.Entities;
using Template.Enums;

public interface IRentalService
{
    public Task<List<DetailsRentalDto>> GetAllAsync();
    public Task<DetailsRentalDto?> UpdateStatusAsync(int id, RentalStatus status);
    public Task<List<DetailsRentalDto>?> UpdateRentalStatusDto(UpdateRentalStatusDto dto);
    public Task<DetailsRentalDto> CreateAsync(CreateRentalDto rentalDto);
}