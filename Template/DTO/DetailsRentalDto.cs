namespace Template.DTO
{
    public class DetailsRentalDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int VehicleId { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string? Status { get; set; }
    }
}