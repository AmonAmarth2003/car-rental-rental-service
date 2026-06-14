using Template.DTO;
using Template.Entities;
using Template.Enums;

namespace Template.Mappers

{
    public static class RentalMapper
    {
        public static DetailsRentalDto ToDetailsDto(Rental rental)
        {
            return new DetailsRentalDto
            {
                Id = rental.Id,
                ClientId = rental.ClientId,
                VehicleId = rental.VehicleId,
                PickupDate = rental.PickupDate,
                ReturnDate = rental.ReturnDate,
                Status = rental.Status.ToString()
            };
        }

        public static Rental ToEntity(CreateRentalDto rentalDto)
        {
            return new Rental
            {
                ClientId = rentalDto.ClientId,
                VehicleId = rentalDto.VehicleId,
                PickupDate = rentalDto.PickupDate,
                ReturnDate = rentalDto.ReturnDate
            };
        }
    }
}
