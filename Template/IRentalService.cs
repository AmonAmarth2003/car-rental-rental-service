using Template.DTO;

public interface IRentalService
{
    public Task<List<DetailsRentalDto>> GetAllAsync();
    public Task<List<DetailsRentalDto>?> UpdateRentalStatus(UpdateRentalStatusDto dto);
    public Task<List<DetailsRentalDto>?> UpdateRentalByVehicleStatus(UpdateVehicleStatusDto dto);
    public Task<DetailsRentalDto> CreateAsync(CreateRentalDto rentalDto);
}