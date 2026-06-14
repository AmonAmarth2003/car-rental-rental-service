using Template.Enums;

namespace Template.DTO
{
    public class CreateRentalDto
    {
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
