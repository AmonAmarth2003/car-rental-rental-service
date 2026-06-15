using Template.Enums;

namespace Template.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int VehicleId{ get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public RentalStatus Status { get; set; }
    }
}
